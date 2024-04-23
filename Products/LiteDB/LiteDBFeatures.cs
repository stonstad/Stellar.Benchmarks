using LiteDB;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Stellar.Benchmarking
{
    public static class LiteDBFeatures
    {
        public static async Task Test(int n)
        {
            string fileName = "lite.db";
            if (File.Exists(fileName))
                File.Delete(fileName);

            string connectionString;

            // test: default
            connectionString = $"Filename={fileName};"; 
            TestFeatures("Default", fileName, connectionString, TestData.CreateCustomers(n));

            // test: encrypted
            connectionString = $"Filename={fileName};Password=password";
            TestFeatures("Encrypted", fileName, connectionString, TestData.CreateCustomers(n));

            await Task.CompletedTask;
        }

        public static void TestFeatures(string test, string fileName, string connectionString, List<Customer> testData)
        {
            Stopwatch stopwatch = new Stopwatch();
            Random random = new Random(TestData.Seed1); // ensure randomization is deterministic for benchmarking

            TestOutput.WriteTestHeader("LiteDB", test, testData.Count);

            LiteDatabase liteDB = new LiteDatabase(connectionString);
            var customers = liteDB.GetCollection<Customer>();

            // insert
            stopwatch.Restart();
            foreach (Customer customer in testData)
                customers.Insert(customer);
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Insert", testData.Count, stopwatch);

            // delete
            stopwatch.Restart();
            foreach (Customer customer in testData)
                customers.Delete(customer.Id);
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Delete", testData.Count, stopwatch);

            // bulk
            stopwatch.Restart();
            customers.InsertBulk(testData);
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Bulk", testData.Count, stopwatch);

            // upsert
            stopwatch.Restart();
            foreach (Customer customer in testData)
            {
                customer.Telephone = random.Next(1000000, 9999999);
                customers.Upsert(customer);
            }
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Upsert", testData.Count, stopwatch);

            // query
            stopwatch.Restart();
            int count = customers.Find(a => a.Name.StartsWith("John") && a.Telephone > 5555555).Count();
            stopwatch.Stop();
            TestOutput.WriteThroughputResult("Query", testData.Count, stopwatch, detail: $"{count.ToString("N0")} matches");

            // close
            stopwatch.Restart();
            liteDB.Dispose();
            stopwatch.Stop();
            TestOutput.WriteTimingResult("Close", stopwatch.ElapsedMilliseconds);

            // reopen database and confirm data integrity
            liteDB = new LiteDatabase(connectionString);
            customers = liteDB.GetCollection<Customer>();
            stopwatch = Stopwatch.StartNew();
            count = 0;
            Dictionary<int, Customer> allCustomers = customers.FindAll().ToDictionary(a => a.Id, a => a);
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

            liteDB.Dispose();

            if (File.Exists(fileName))
                File.Delete(fileName);

            Console.WriteLine();
        }
    }
}
