﻿using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Stellar.Benchmarking
{
    public class SQLiteTests : IBenchmark
    {
        private List<Customer> _TestData;
        private Random _Random = new Random(TestData.Seed0);

        // SQLite
        private SqliteConnection _Connection;
        private string _FileName;

        public SQLiteTests()
        {
        }

        public void Setup(List<Customer> testData)
        {
            _TestData = testData;

            _FileName = Path.Combine(Environment.CurrentDirectory, "sqlite.db");
            if (File.Exists(_FileName))
            {
                try
                {
                    File.Delete(_FileName);
                }
                catch
                {
                    if (File.Exists(_FileName)) // System.Data.SQLite refuses to release file handles
                        _FileName = Path.Combine(Environment.CurrentDirectory, Path.GetRandomFileName());
                }
            }

            _Connection = new SqliteConnection($"Data Source={_FileName}");
            _Connection.Open();

            using (var cmd = _Connection.CreateCommand())
            {
                cmd.CommandText =
                    @"CREATE TABLE IF NOT EXISTS Customers 
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
            foreach (Customer customer in _TestData)
                using (var cmd = _Connection.CreateCommand())    // must recreate command for each loop iteration
                {
                    cmd.CommandText = $"INSERT INTO Customers VALUES (@Id, @Name, @Telephone, @DateOfBirth);";
                    cmd.Parameters.AddWithValue("@Id", customer.Id);
                    cmd.Parameters.AddWithValue("@Name", customer.Name);
                    cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                    cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                    cmd.ExecuteNonQuery();
                }
        }

        public void Bulk()
        {
            using (var trans = _Connection.BeginTransaction())
            {
                foreach (Customer customer in _TestData)
                    using (var cmd = _Connection.CreateCommand())    // must recreate command for each loop iteration
                    {
                        cmd.CommandText = $"INSERT INTO Customers VALUES (@Id, @Name, @Telephone, @DateOfBirth);";
                        cmd.Parameters.AddWithValue("@Id", customer.Id);
                        cmd.Parameters.AddWithValue("@Name", customer.Name);
                        cmd.Parameters.AddWithValue("@Telephone", customer.Telephone);
                        cmd.Parameters.AddWithValue("@DateOfBirth", customer.DateOfBirth);
                        cmd.ExecuteNonQuery();
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
            foreach (Customer customer in _TestData)
                using (var cmd = _Connection.CreateCommand())  // must recreate command for each loop iteration
                {
                    customer.Telephone = _Random.Next(1000000, 9999999);
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
