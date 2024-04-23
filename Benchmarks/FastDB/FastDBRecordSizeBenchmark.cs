using BenchmarkDotNet.Attributes;
using Stellar.Collections;
using System;
using System.Collections.Generic;
using System.IO;

namespace Stellar.Benchmarking
{
    public class FastDBRecordSizeBenchmark
    {
        private const int N = Constants.N_10_000;
        private string _BenchmarkName;
        
        [Params("FastDB")]
        public string Product { get; set; }

        private FastDB _FastDB;
        private IFastDBCollection<int, Customer> _Customers;

        private List<Customer> _CustomersTestData;

        public void IterationSetup(string benchmark)
        {
            _BenchmarkName = benchmark;

            string file = Path.Combine(new FastDBOptions().DirectoryPath, "Customer.db");
            if (File.Exists(file))
                File.Delete(file);

            switch (_BenchmarkName)
            {
                case nameof(Large):
                    {
                        FastDBOptions options = new FastDBOptions();
                        _FastDB = new FastDB(options);
                        _CustomersTestData = TestData.CreateCustomersLongText(N);
                        _Customers = _FastDB.GetCollection<int, Customer>();
                        break;
                    }
                case nameof(LargeEncrypted):
                    {
                        FastDBOptions options = new FastDBOptions()
                        {
                            IsEncryptionEnabled = true,
                            EncryptionPassword = "open-sesame",
                        };
                        _FastDB = new FastDB(options);
                        _CustomersTestData = TestData.CreateCustomersLongText(N);
                        _Customers = _FastDB.GetCollection<int, Customer>();
                        break;
                    }
                case nameof(LargeEncryptedCompressed):
                    {
                        FastDBOptions options = new FastDBOptions()
                        {
                            IsEncryptionEnabled = true,
                            IsCompressionEnabled = true,
                            EncryptionPassword = "open-sesame",
                        };
                        _FastDB = new FastDB(options);
                        _CustomersTestData = TestData.CreateCustomersLongText(N);
                        _Customers = _FastDB.GetCollection<int, Customer>();
                        break;
                    }
                case nameof(LargeEncryptedCompressedParallel):
                    {
                        FastDBOptions options = new FastDBOptions()
                        {
                            BufferMode = BufferModeType.WriteParallelEnabled,
                            MaxDegreeOfParallelism = 8,

                            IsEncryptionEnabled = true,
                            IsCompressionEnabled = true,
                            EncryptionPassword = "open-sesame",
                        };
                        _FastDB = new FastDB(options);
                        _CustomersTestData = TestData.CreateCustomersLongText(N);
                        _Customers = _FastDB.GetCollection<int, Customer>();
                        break;
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        [IterationSetup(Target = nameof(Large))]
        public void IterationSetupLarge()
        {
            IterationSetup(nameof(Large));
        }

        [IterationSetup(Target = nameof(LargeEncrypted))]
        public void IterationSetupLargeEncrypted()
        {
            IterationSetup(nameof(LargeEncrypted));
        }

        [IterationSetup(Target = nameof(LargeEncryptedCompressed))]
        public void IterationSetupEncryptedCompressed()
        {
            IterationSetup(nameof(LargeEncryptedCompressed));
        }

        [IterationSetup(Target = nameof(LargeEncryptedCompressedParallel))]
        public void IterationSetupEncryptedCompressedParallel()
        {
            IterationSetup(nameof(LargeEncryptedCompressedParallel));
        }


        [IterationCleanup()]
        public void IterationCleanup()
        {
            BenchmarkMetadata.Instance.AddMetadata("FileSize", $"{_FastDB.GetFileSizeBytes() / 1024} KB");
            BenchmarkMetadata.Instance.Save($"{_BenchmarkName}_{Product}");
            _FastDB.Close();
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void Large()
        {
            foreach (Customer customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void LargeEncrypted()
        {
            foreach (Customer customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void LargeEncryptedCompressed()
        {
            foreach (Customer customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
        }

        [Benchmark(OperationsPerInvoke = N)]
        public void LargeEncryptedCompressedParallel()
        {
            foreach (Customer customer in _CustomersTestData)
                _Customers.Add(customer.Id, customer);
            _Customers.Flush();
        }
    }
}
