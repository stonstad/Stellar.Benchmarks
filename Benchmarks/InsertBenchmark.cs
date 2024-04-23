using BenchmarkDotNet.Attributes;
using System.Collections.Generic;

namespace Stellar.Benchmarking
{
    public class InsertBenchmark
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
        }

        [IterationSetup(Target = nameof(Insert1000))]
        public void IterationSetup_Insert1000()
        {
            _BenchmarkName = nameof(Insert1000);
            IterationSetup(TestData.CreateCustomers(Constants.N_1_000));
        }

        [IterationSetup(Target = nameof(Insert5000))]
        public void IterationSetup_Insert5000()
        {
            _BenchmarkName = nameof(Insert5000);
            IterationSetup(TestData.CreateCustomers(Constants.N_5_000));
        }

        [IterationSetup(Target = nameof(Insert10000))]
        public void IterationSetup_Insert10000()
        {
            _BenchmarkName = nameof(Insert10000);
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
        public void Insert1000()
        {
            _Product.Insert();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_5_000)]
        public void Insert5000()
        {
            _Product.Insert();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_10_000)]
        public void Insert10000()
        {
            _Product.Insert();
        }
    }
}
