using BenchmarkDotNet.Attributes;
using Stellar.Collections;
using System;
using System.Collections.Generic;
using System.IO;

namespace Stellar.Benchmarking
{
    public class FastDBPerformanceBenchmark
    {
        private const int N = Constants.N_10_000;
        private string _BenchmarkName;

        [Params("FastDB")]
        public string Product { get; set; }

        private FastDB _FastDB;
        private IFastDBCollection<int, Customer> _Customers;
        private IFastDBCollection<int, CustomerWithContract> _CustomersWithContract;

        private List<Customer> _CustomersTestData;
        private List<CustomerWithContract> _CustomersWithContractTestData;

        public void IterationSetup(string benchmark)
        {
            _BenchmarkName = benchmark;

            string file = Path.Combine(new FastDBOptions().DirectoryPath, "Customer.db");
            if (File.Exists(file))
                File.Delete(file);

            file = Path.Combine(new FastDBOptions().DirectoryPath, "CustomerWithContract.db");
            if (File.Exists(file))
                File.Delete(file);

            switch (_BenchmarkName)
            {
                case nameof(Default):
                    {
                        FastDBOptions options = new FastDBOptions();
                        _FastDB = new FastDB(options);

                        _CustomersTestData = TestData.CreateCustomers(N);
                        _Customers = _FastDB.GetCollection<int, Customer>();
                        break;
                    }
                case nameof(Contract):
                    {
                        FastDBOptions options = new FastDBOptions()
                        {
                            Serializer = SerializerType.MessagePack_Contract
                        };
                        _FastDB = new FastDB(options);

                        _CustomersWithContractTestData = TestData.CreateCustomersWithSerializationContract(N);
                        _CustomersWithContract = _FastDB.GetCollection<int, CustomerWithContract>();

                        break;
                    }
                case nameof(Parallel):
                    {
                        FastDBOptions options = new FastDBOptions()
                        {
                            Serializer = SerializerType.MessagePack_Contract,
                            BufferMode = BufferModeType.WriteParallelEnabled,
                            MaxDegreeOfParallelism = 12,
                        };
                        _FastDB = new FastDB(options);
                        _CustomersWithContractTestData = TestData.CreateCustomersWithSerializationContract(N);
                        _CustomersWithContract = _FastDB.GetCollection<int, CustomerWithContract>();
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [IterationSetup(Target = nameof(Default))]
        public void IterationSetupDefault()
        {
            IterationSetup(nameof(Default));
        }

        [IterationSetup(Target = nameof(Contract))]
        public void IterationSetupContract()
        {
            IterationSetup(nameof(Contract));
        }

        [IterationSetup(Target = nameof(Parallel))]
        public void IterationSetupParallel()
        {
            IterationSetup(nameof(Parallel));
        }

        [IterationCleanup()]
        public void IterationCleanup()
        {
            BenchmarkMetadata.Instance.AddMetadata("FileSize", $"{_FastDB.GetFileSizeBytes() / 1024} KB");
            BenchmarkMetadata.Instance.Save($"{_BenchmarkName}_{Product}");
            _FastDB.Close();
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void Default()
        {
            foreach (Customer customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void Contract()
        {
            foreach (CustomerWithContract customer in _CustomersWithContractTestData)
                _CustomersWithContract.Add(customer.Id, customer);
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void Parallel()
        {
            foreach (CustomerWithContract customer in _CustomersWithContractTestData)
                _CustomersWithContract.Add(customer.Id, customer);
            _CustomersWithContract.Flush();
        }
    }
}
