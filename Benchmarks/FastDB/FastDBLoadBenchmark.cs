using BenchmarkDotNet.Attributes;
using Stellar.Collections;
using System;
using System.IO;
using System.Linq;

namespace Stellar.Benchmarking
{
    /// <summary>
    /// Measures how fast an existing database is opened (records loaded per second).
    /// </summary>
    public class FastDBLoadBenchmark
    {
        private const string DatabaseName = "FastDB_Load";
        private string _BenchmarkName;
        private int _ExpectedRecords;

        [Params("FastDB")]
        public string Product { get; set; }

        private FastDB _FastDB;
        private IFastDBCollection<int, Customer> _Customers;

        private static FastDBOptions CreateOptions() => new FastDBOptions() { DatabaseName = DatabaseName };

        private static void DeleteDatabase()
        {
            string directory = CreateOptions().DirectoryPath;
            if (Directory.Exists(directory))
                Directory.Delete(directory, true);
        }

        private void CreateDatabase(int records)
        {
            DeleteDatabase();
            _ExpectedRecords = records;

            FastDB fastDB = new FastDB(CreateOptions());
            var customers = fastDB.GetCollection<int, Customer>();
            customers.AddBulkAsync(TestData.CreateCustomers(records).ToDictionary(a => a.Id, a => a)).AsTask().GetAwaiter().GetResult();
            fastDB.Close();
        }

        [GlobalSetup(Target = nameof(Load100000))]
        public void GlobalSetup_Load100000()
        {
            CreateDatabase(Constants.N_100_000);
        }

#if (IncludeMillion)
        [GlobalSetup(Target = nameof(Load1000000))]
        public void GlobalSetup_Load1000000()
        {
            CreateDatabase(Constants.N_1_000_000);
        }
#endif

        [GlobalCleanup]
        public void GlobalCleanup()
        {
            DeleteDatabase();
        }

        [IterationSetup(Target = nameof(Load100000))]
        public void IterationSetup_Load100000()
        {
            _BenchmarkName = nameof(Load100000);
            _FastDB = new FastDB(CreateOptions());
        }

#if (IncludeMillion)
        [IterationSetup(Target = nameof(Load1000000))]
        public void IterationSetup_Load1000000()
        {
            _BenchmarkName = nameof(Load1000000);
            _FastDB = new FastDB(CreateOptions());
        }
#endif

        [IterationCleanup]
        public void IterationCleanup()
        {
            if (_Customers.Count != _ExpectedRecords)
                throw new InvalidOperationException($"Expected {_ExpectedRecords} records after load, found {_Customers.Count}.");

            BenchmarkMetadata.Instance.AddMetadata("FileSize", $"{(_FastDB.GetFileSizeBytes() / 1024).ToString("N0")} KB");
            BenchmarkMetadata.Instance.Save($"{_BenchmarkName}_{Product}");
            _FastDB.Close();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_100_000)]
        public void Load100000()
        {
            _Customers = _FastDB.GetCollection<int, Customer>();
        }

#if (IncludeMillion)
        [Benchmark(OperationsPerInvoke = Constants.N_1_000_000)]
        public void Load1000000()
        {
            _Customers = _FastDB.GetCollection<int, Customer>();
        }
#endif
    }
}
