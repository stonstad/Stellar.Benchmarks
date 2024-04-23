#if (VistaDB)
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VistaDB.Provider;

namespace Stellar.Benchmarking
{
    public static class VistaDBFeatures
    {
        public static async Task Test(int n)
        {
            string fileName = $"vista.db";
            if (File.Exists(fileName))
                File.Delete(fileName);

            // test: default
            string connectionString = $"Data Source={fileName}";
            await TestFeatures("Default", fileName, connectionString, TestData.CreateCustomers(n));
        }

        public static async Task TestFeatures(string test, string fileName, string connectionString, List<Customer> testData)
        {
            Stopwatch stopwatch = new Stopwatch();
            Random random = new Random(TestData.Seed1); // ensure randomization is deterministic for benchmarking

            TestOutput.WriteTestHeader("VistaDB", test, testData.Count);

            // register required code page for Vista .NET core support
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            // create database
            VistaDBConnection connection = new VistaDBConnection(connectionString);

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = $"CREATE DATABASE '{fileName}', PAGE SIZE 4, LCID 1033, CASE SENSITIVE FALSE;";
                await cmd.ExecuteNonQueryAsync();
            }

            // create table
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText =
                    @"CREATE TABLE Customers 
                (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Telephone INTEGER, DateOfBirth TEXT);";
                await cmd.ExecuteNonQueryAsync();
            }

            // insert
            stopwatch.Restart();
            foreach (Customer customer in testData)
                using (var cmd = connection.CreateCommand())
                {
                    cmd.CommandText = $"INSERT INTO Customers VALUES (@Id, @Name, @Telephone, @DateOfBirth);";
                    cmd.Parameters.AddWithValue("@Id", customer.Id);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                    cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                    await cmd.ExecuteNonQueryAsync();
                }
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Insert", testData.Count, stopwatch);

            // delete
            stopwatch.Restart();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM Customers";
                await cmd.ExecuteNonQueryAsync();
            }
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Delete", testData.Count, stopwatch);

            // bulk
            stopwatch.Restart();
            using (var trans = connection.BeginTransaction())
            {
                using (var cmd = connection.CreateCommand())
                    foreach (Customer customer in testData)
                    {
                        cmd.CommandText = $"INSERT INTO Customers VALUES (@Id, @Name, @Telephone, @DateOfBirth);";
                        cmd.Parameters.AddWithValue("@Id", customer.Id);
                        cmd.Parameters.AddWithValue("@Name", customer.Name);
                        cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                        cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                        await cmd.ExecuteNonQueryAsync();
                    }
                trans.Commit();
            }
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Bulk", testData.Count, stopwatch);

            // upsert
            stopwatch.Restart();
            using (var cmd = connection.CreateCommand())
                foreach (Customer customer in testData)
                {
                    cmd.CommandText = @"
                    UPDATE Customers
                    SET 
                        Name = @Name,
                        Telephone = @Telephone,
                        DateOfBirth = @DateOfBirth
                        WHERE Id = @Id;

                    IF @@ROWCOUNT = 0
                    BEGIN
                        INSERT INTO Customers (Id, Name, Telephone, DateOfBirth)
                        VALUES (@Id, @Name, @Telephone, @DateOfBirth);
                    END";
                    cmd.Parameters.AddWithValue("@Id", customer.Id);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                    cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                    await cmd.ExecuteNonQueryAsync();
                }
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Upsert", testData.Count, stopwatch);

            // query
            int count = 0;
            stopwatch.Restart();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Customers WHERE Name LIKE 'John%' AND Telephone > 5555555";
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (reader.Read())
                        count++;
            }
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Query", testData.Count, stopwatch, detail: $"{count.ToString("N0")} matches");

            // close
            stopwatch.Restart();
            connection.Close();
            connection.Dispose();

            stopwatch.Stop();
            TestOutput.WriteTimingResult("Close", stopwatch.ElapsedMilliseconds);

            // reopen database and confirm data integrity
            count = 0;
            connection = new VistaDBConnection(connectionString);
            connection.Open();
            Dictionary<int, Customer> testDataCustomers = testData.ToDictionary(a => a.Id, a => a);
            Dictionary<int, Customer> allCustomers = new Dictionary<int, Customer>();
            stopwatch = Stopwatch.StartNew();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Customers";
                using (var reader = await cmd.ExecuteReaderAsync())
                    while (reader.Read())
                    {
                        int id = reader.GetInt32(0);
                        string name = reader.GetString(1);
                        int telephone = reader.GetInt32(2);
                        DateTime dob = reader.GetDateTime(3);
                        allCustomers.Add(id, new Customer() { Id = id, Name = name, Telephone = telephone, DateOfBirth = dob });
                    }
            }
            foreach (Customer customer in testData)
            {
                count++;
                if (!customer.Equals(allCustomers[customer.Id]))
                    throw new Exception("Data mismatch encountered");
            }
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Iteration", count, stopwatch, detail: $"{count.ToString("N0")} records");

            // file size
            long fileSizeBytes = new FileInfo(fileName).Length;
            TestOutput.WriteFileSizeBytes("File Size", fileSizeBytes);

            connection.Close();
            connection.Dispose();

            File.Delete(fileName);

            Console.WriteLine();
        }
    }
}
#endif