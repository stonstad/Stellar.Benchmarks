using BenchmarkDotNet.Attributes;
using Stellar.Collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Stellar.Benchmarking
{
    /// <summary>
    /// Total insert throughput when several threads write to the same collection at once.
    /// </summary>
    public class FastDBConcurrentWriteBenchmark
    {
        private const string DatabaseName = "FastDB_Concurrent";
        private const int N = Constants.N_100_000;
        private string _BenchmarkName;

        [Params("FastDB")]
        public string Product { get; set; }

        private FastDB _FastDB;
        private IFastDBCollection<int, Customer> _Customers;
        private List<Customer> _TestData;

        private void IterationSetup(string benchmark)
        {
            _BenchmarkName = benchmark;

            FastDBOptions options = new FastDBOptions() { DatabaseName = DatabaseName };
            if (Directory.Exists(options.DirectoryPath))
                Directory.Delete(options.DirectoryPath, true);

            _FastDB = new FastDB(options);
            _Customers = _FastDB.GetCollection<int, Customer>();
            _TestData ??= TestData.CreateCustomers(N);
        }

        [IterationSetup(Target = nameof(Writers1))]
        public void IterationSetup_Writers1()
        {
            IterationSetup(nameof(Writers1));
        }

        [IterationSetup(Target = nameof(Writers4))]
        public void IterationSetup_Writers4()
        {
            IterationSetup(nameof(Writers4));
        }

        [IterationSetup(Target = nameof(Writers16))]
        public void IterationSetup_Writers16()
        {
            IterationSetup(nameof(Writers16));
        }

        [IterationCleanup]
        public void IterationCleanup()
        {
            _Customers.Flush();
            if (_Customers.Count != N)
                throw new InvalidOperationException($"Expected {N} records, found {_Customers.Count}.");

            BenchmarkMetadata.Instance.AddMetadata("FileSize", $"{(_FastDB.GetFileSizeBytes() / 1024).ToString("N0")} KB");
            BenchmarkMetadata.Instance.Save($"{_BenchmarkName}_{Product}");
            _FastDB.Close();
        }

        // each writer inserts an interleaved slice of the test data
        private void Write(int writers)
        {
            Thread[] threads = new Thread[writers];
            for (int t = 0; t < writers; t++)
            {
                int start = t;
                threads[t] = new Thread(() =>
                {
                    for (int i = start; i < N; i += writers)
                        _Customers.Add(_TestData[i].Id, _TestData[i]);
                });
            }

            foreach (Thread thread in threads)
                thread.Start();
            foreach (Thread thread in threads)
                thread.Join();
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void Writers1()
        {
            Write(1);
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void Writers4()
        {
            Write(4);
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void Writers16()
        {
            Write(16);
        }
    }
}
