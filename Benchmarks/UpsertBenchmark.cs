using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Stellar.Benchmarking
{
    public class UpsertBenchmark
    {
        public IEnumerable<string> Products => ProductFactory.AvailableProducts;

        [ParamsSource(nameof(Products))]
        public string Product { get; set; }

        private IBenchmark _Product;

        private string _BenchmarkName;

        private void IterationSetup(List<Customer> testData)
        {
            _Product = ProductFactory.Create(Product);
            _Product.Setup(testData);
            _Product.Insert(); // create test data to subsequently delete. This operation is not benchmarked
        }

        [IterationSetup(Target = nameof(Upsert1000))]
        public void IterationSetup_Upsert1000()
        {
            _BenchmarkName = nameof(Upsert1000);
            IterationSetup(TestData.CreateCustomers(Constants.N_1_000));
        }

        [IterationSetup(Target = nameof(Upsert5000))]
        public void IterationSetup_Upsert5000()
        {
            _BenchmarkName = nameof(Upsert5000);
            IterationSetup(TestData.CreateCustomers(Constants.N_5_000));
        }

        [IterationSetup(Target = nameof(Upsert10000))]
        public void IterationSetup_Upsert10000()
        {
            _BenchmarkName = nameof(Upsert10000);
            IterationSetup(TestData.CreateCustomers(Constants.N_10_000));
        }

        [IterationCleanup()]
        public void IterationCleanup()
        {
            BenchmarkMetadata.Instance.AddMetadata("FileSize", $"{_Product.GetFileSizeBytes() / 1024} KB");
            BenchmarkMetadata.Instance.Save($"{_BenchmarkName}_{Product}");
            _Product.Cleanup();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_1_000)]
        public void Upsert1000()
        {
            _Product.Upsert();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_5_000)]
        public void Upsert5000()
        {
            _Product.Upsert();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_10_000)]
        public void Upsert10000()
        {
            _Product.Upsert();
        }
    }
}
