using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Stellar.Benchmarking
{
    public static class SQLiteFeatures
    {
        public static async Task Test(int n)
        {
            string fileName = $"sqlite.db";
            if (File.Exists(fileName))
            {
                try
                {
                    File.Delete(fileName);
                }
                catch
                {
                    if (File.Exists(fileName)) // System.Data.SQLite refuses to release file handles
                        fileName = Path.Combine(Environment.CurrentDirectory, Path.GetRandomFileName());
                }
            }

            // test: default
            string connectionString = $"Data Source={fileName}";
            await TestFeatures("Default", fileName, connectionString, TestData.CreateCustomers(n));
        }

        public static async Task TestFeatures(string test, string fileName, string connectionString, List<Customer> testData)
        {
            Stopwatch stopwatch = new Stopwatch();
            Random random = new Random(TestData.Seed1); // ensure randomization is deterministic for benchmarking

            TestOutput.WriteTestHeader("SQLite", test, testData.Count);

            SqliteConnection connection = new SqliteConnection(connectionString);
            connection.Open();

            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS Customers 
                (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Telephone INTEGER, DateOfBirth TEXT);";
                await cmd.ExecuteNonQueryAsync();
            }

            // bulk
            stopwatch.Restart();
            using (var trans = connection.BeginTransaction())
            {
                foreach (Customer customer in testData)
                    using (var cmd = connection.CreateCommand())  // must recreate command for each loop iteration
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

            // delete
            stopwatch.Restart();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM Customers";
                await cmd.ExecuteNonQueryAsync();
            }
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Delete", testData.Count, stopwatch);

            // insert
            stopwatch.Restart();
            foreach (Customer customer in testData)
                using (var cmd = connection.CreateCommand()) // must recreate command for each loop iteration
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

            // upsert
            stopwatch.Restart();
            foreach (Customer customer in testData)
                using (var cmd = connection.CreateCommand())   // must recreate command for each loop iteration
                {
                    customer.Telephone = random.Next(1000000, 9999999);
                    cmd.CommandText = @"
                    INSERT INTO Customers (Id, Name, Telephone, DateOfBirth)
                    VALUES (@Id, @Name, @Telephone, @DateOfBirth)
                    ON CONFLICT(Id)
                    DO UPDATE SET
                    Name = excluded.Name,
                    Telephone = excluded.Telephone,
                    DateOfBirth = excluded.DateOfBirth";
                    cmd.Parameters.AddWithValue("@Id", customer.Id);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                    cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                    await cmd.ExecuteNonQueryAsync();
                }
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Upsert", testData.Count, stopwatch);

            // query and iterate
            List<Customer> customers = new List<Customer>();
            stopwatch.Restart();
            using (var cmd = connection.CreateCommand())
            {
                cmd.CommandText =
                   @"SELECT Id, Name, Telephone, DateOfBirth FROM Customers 
                   WHERE Name LIKE 'John%' AND Telephone > 5555555";
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                    {
                        Customer customer = new Customer();
                        customer.Id = reader.GetInt32(0);
                        customer.Name = reader.GetString(1);
                        customer.Telephone = reader.GetInt32(2);
                        customer.DateOfBirth = reader.GetDateTime(3);
                        customers.Add(customer);
                    }
            }
            int value = 0;
            int count = 0;
            foreach (Customer customer in customers)
            {
                value += customer.Id;
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
            connection = new SqliteConnection(connectionString);
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

            Console.WriteLine();
        }
    }
}