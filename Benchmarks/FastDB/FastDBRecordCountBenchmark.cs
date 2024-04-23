using BenchmarkDotNet.Attributes;
using Stellar.Collections;
using System.Collections.Generic;
using System.IO;

namespace Stellar.Benchmarking
{
    public class FastDBRecordCountBenchmark
    {
        private string _BenchmarkName;

        private FastDB _FastDB;
        private IFastDBCollection<int, CustomerWithContract> _Customers;

        private List<CustomerWithContract> _CustomersTestData;

        public void IterationSetup(string benchmark, int records)
        {
            _BenchmarkName = benchmark;

            string file = Path.Combine(new FastDBOptions().DirectoryPath, "CustomerWithContract.db");
            if (File.Exists(file))
                File.Delete(file);

            FastDBOptions options = new FastDBOptions()
            {
                Serializer = SerializerType.MessagePack_Contract,
            };
            _FastDB = new FastDB(options);

            _CustomersTestData = TestData.CreateCustomersWithSerializationContract(records);
            _Customers = _FastDB.GetCollection<int, CustomerWithContract>();
        }


        [IterationSetup(Target = nameof(Insert1000))]
        public void IterationSetup_Insert1000()
        {
            IterationSetup(nameof(Insert1000), Constants.N_1_000);
        }

#if(IncludeHalfIncrement)
        [IterationSetup(Target = nameof(Insert5000))]
        public void IterationSetup_Insert5000()
        {
            IterationSetup(nameof(Insert5000), Constants.N_5_000);
        }
#endif

        [IterationSetup(Target = nameof(Insert10000))]
        public void IterationSetup_Insert10000()
        {
            IterationSetup(nameof(Insert10000), Constants.N_10_000);
        }

#if(IncludeHalfIncrement)
        [IterationSetup(Target = nameof(Insert50000))]
        public void IterationSetup_Insert50000()
        {
            IterationSetup(nameof(Insert50000), Constants.N_50_000);
        }
#endif

        [IterationSetup(Target = nameof(Insert100000))]
        public void IterationSetup_Insert100000()
        {
            IterationSetup(nameof(Insert100000), Constants.N_100_000);
        }

#if (IncludeHalfIncrement)
        [IterationSetup(Target = nameof(Insert500000))]
        public void IterationSetup_Insert500000()
        {
            IterationSetup(nameof(Insert500000), Constants.N_500_000);
        }
#endif


        [IterationSetup(Target = nameof(Insert1000000))]
        public void IterationSetup_Insert1000000()
        {
            IterationSetup(nameof(Insert1000000), Constants.N_1_000_000);
        }

        [IterationCleanup()]
        public void IterationCleanup()
        {
            BenchmarkMetadata.Instance.AddMetadata("FileSize", $"{_FastDB.GetFileSizeBytes() / 1024} KB");
            BenchmarkMetadata.Instance.Save($"{_BenchmarkName}");
            _FastDB.Close();
        }


        [Benchmark(OperationsPerInvoke = Constants.N_1_000)]
        public void Insert1000()
        {
            foreach (var customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }

#if (IncludeHalfIncrement)
        [Benchmark(OperationsPerInvoke = Constants.N_5_000)]
        public void Insert5000()
        {
            foreach (var customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }
#endif

        [Benchmark(OperationsPerInvoke = Constants.N_10_000)]
        public void Insert10000()
        {
            foreach (var customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }

#if (IncludeHalfIncrement)
        [Benchmark(OperationsPerInvoke = Constants.N_50_000)]
        public void Insert50000()
        {
            foreach (var customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }
#endif

        [Benchmark(OperationsPerInvoke = Constants.N_100_000)]
        public void Insert100000()
        {
            foreach (var customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }

#if (IncludeHalfIncrement)
        [Benchmark(OperationsPerInvoke = Constants.N_500_000)]
        public void Insert500000()
        {
            foreach (var customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }
#endif

        [Benchmark(OperationsPerInvoke = Constants.N_1_000_000)]
        public void Insert1000000()
        {
            foreach (var customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }
    }
}
