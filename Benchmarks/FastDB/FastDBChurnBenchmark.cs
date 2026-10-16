using BenchmarkDotNet.Attributes;
using Stellar.Collections;
using System;
using System.Diagnostics;
using System.IO;
using System.Threading;

namespace Stellar.Benchmarking
{
    /// <summary>
    /// Long-running update workload with variable record sizes, like a game server rewriting player state.
    /// Reports throughput and the file size before, right after the updates, and at rest once compaction has finished.
    /// </summary>
    public class FastDBChurnBenchmark
    {
        private const string DatabaseName = "FastDB_Churn";
        private const int Records = Constants.N_10_000;
        private const int Updates = Constants.N_50_000;

        [Params("FastDB")]
        public string Product { get; set; }

        private FastDB _FastDB;
        private IFastDBCollection<int, Customer> _Customers;
        private Customer[] _Updates;
        private long _InitialFileSizeBytes;

        // most records are small, roughly one in four is large
        private static Customer CreateVariableSizeCustomer(Random random, int id)
        {
            int length;
            if (random.Next(4) == 0)
                length = random.Next(200, 1200);
            else
                length = random.Next(8, 60);
            char[] name = new char[length];
            for (int i = 0; i < length; i++)
                name[i] = (char)('a' + random.Next(26));

            return new Customer()
            {
                Id = id,
                Name = new string(name),
                Telephone = random.Next(1000000, 9999999),
                DateOfBirth = new DateTime(2000, 1, 1) + TimeSpan.FromDays(random.Next(0, 10000)),
            };
        }

        [IterationSetup(Target = nameof(Churn50000))]
        public void IterationSetup_Churn50000()
        {
            FastDBOptions options = new FastDBOptions() { DatabaseName = DatabaseName };
            if (Directory.Exists(options.DirectoryPath))
                Directory.Delete(options.DirectoryPath, true);

            Random random = new Random(TestData.Seed0);

            _FastDB = new FastDB(options);
            _Customers = _FastDB.GetCollection<int, Customer>();
            for (int id = 1; id <= Records; id++)
                _Customers.Add(id, CreateVariableSizeCustomer(random, id));
            _Customers.Flush();
            _InitialFileSizeBytes = _FastDB.GetFileSizeBytes();

            _Updates = new Customer[Updates];
            for (int i = 0; i < Updates; i++)
                _Updates[i] = CreateVariableSizeCustomer(random, random.Next(1, Records + 1));
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            _Customers.Flush();
            long fileSizeBytes = _FastDB.GetFileSizeBytes();
            long restingFileSizeBytes = FileSizeAtRest();
            BenchmarkMetadata.Instance.AddMetadata("FileSize", $"{(_InitialFileSizeBytes / 1024).ToString("N0")} KB -> " +
                $"{(fileSizeBytes / 1024).ToString("N0")} KB -> {(restingFileSizeBytes / 1024).ToString("N0")} KB at rest");
            BenchmarkMetadata.Instance.Save($"{nameof(Churn50000)}_{Product}");
            _FastDB.Close();
        }

        // background compaction finishes once writes pause, so wait until the file has stopped shrinking for a second
        private long FileSizeAtRest()
        {
            long size = _FastDB.GetFileSizeBytes();
            Stopwatch unchanged = Stopwatch.StartNew();
            Stopwatch waited = Stopwatch.StartNew();
            while (unchanged.ElapsedMilliseconds < 1000 && waited.ElapsedMilliseconds < 10_000)
            {
                Thread.Sleep(50);
                long current = _FastDB.GetFileSizeBytes();
                if (current != size)
                {
                    size = current;
                    unchanged.Restart();
                }
            }
            return size;
        }

        [Benchmark(OperationsPerInvoke = Updates)]
        public void Churn50000()
        {
            foreach (Customer customer in _Updates)
                _Customers.Update(customer.Id, customer);
        }
    }
}
