#if(VistaDB)
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using VistaDB.Provider;

namespace Stellar.Benchmarking
{
    public class VistaDBTests : IBenchmark
    {
        private List<Customer> _TestData;
        private Random _Random = new Random(TestData.Seed0);

        // VisaDB
        private VistaDBConnection _Connection;
        private string _FileName;

        public VistaDBTests()
        {
        }

        public void Setup(List<Customer> testData)
        {
            _TestData = testData;

            _FileName = Path.Combine(Environment.CurrentDirectory, "vista.db");
            if (File.Exists(_FileName))
                File.Delete(_FileName);

            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

            _Connection = new VistaDBConnection($"Data Source={_FileName}");

            using (var cmd = _Connection.CreateCommand())
            {
                cmd.CommandText = $"CREATE DATABASE '{_FileName}', PAGE SIZE 4, LCID 1033, CASE SENSITIVE FALSE;";
                cmd.ExecuteNonQuery();
            }

            using (var cmd = _Connection.CreateCommand())
            {
                cmd.CommandText =
                    @"CREATE TABLE Customers 
                    (Id INTEGER PRIMARY KEY, Name TEXT NOT NULL, Telephone INTEGER, DateOfBirth TEXT);";
                cmd.ExecuteNonQuery();
            }
        }

        public void Cleanup()
        {
            _Connection.Close();
            _Connection.Dispose();
        }

        public void Insert()
        {
            using (var cmd = _Connection.CreateCommand())
                foreach (Customer customer in _TestData)
                {
                    cmd.CommandText = $"INSERT INTO Customers VALUES (@Id, @Name, @Telephone, @DateOfBirth);";
                    cmd.Parameters.AddWithValue("@Id", customer.Id);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                    cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                    cmd.ExecuteNonQueryAsync();
                }
        }

        public void Bulk()
        {
            using (var trans = _Connection.BeginTransaction())
            {
                using (var cmd = _Connection.CreateCommand())
                    foreach (Customer customer in _TestData)
                    {
                        cmd.CommandText = $"INSERT INTO Customers VALUES (@Id, @Name, @Telephone, @DateOfBirth);";
                        cmd.Parameters.AddWithValue("@Id", customer.Id);
                        cmd.Parameters.AddWithValue("@Name", customer.Name);
                        cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                        cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                        cmd.ExecuteNonQueryAsync();
                    }
                trans.Commit();
            }
        }

        public void Delete()
        {
            using (var cmd = _Connection.CreateCommand())
            {
                cmd.CommandText = @"DELETE FROM Customers";
                cmd.ExecuteNonQuery();
            }
        }

        public void Upsert()
        {
            using (var cmd = _Connection.CreateCommand())
                foreach (Customer customer in _TestData)
                {
                    customer.Telephone = _Random.Next(1000000, 9999999);
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
                    cmd.ExecuteNonQuery();
                }
        }

        public void Query()
        {
            int count = 0;
            using (var cmd = _Connection.CreateCommand())
            {
                cmd.CommandText = "SELECT * FROM Customers WHERE Name LIKE 'John%' AND Telephone > 5555555";
                using (var reader = cmd.ExecuteReader())
                    while (reader.Read())
                        count++;
            }
        }

        public long GetFileSizeBytes()
        {
            if (_FileName != null)
                return new FileInfo(_FileName).Length;
            else
                return -1;
        }
    }
}
#endif