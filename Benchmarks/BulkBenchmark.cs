using BenchmarkDotNet.Attributes;
using Stellar.Collections;
using System;
using System.Collections.Generic;

namespace Stellar.Benchmarking
{
    public class BulkBenchmark
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

        [IterationSetup(Target = nameof(Bulk1000))]
        public void IterationSetup_Bulk1000()
        {
            _BenchmarkName = nameof(Bulk1000);
            IterationSetup(TestData.CreateCustomers(Constants.N_1_000));
        }

        [IterationSetup(Target = nameof(Bulk5000))]
        public void IterationSetup_Bulk5000()
        {
            _BenchmarkName = nameof(Bulk5000);
            IterationSetup(TestData.CreateCustomers(Constants.N_5_000));
        }

        [IterationSetup(Target = nameof(Bulk10000))]
        public void IterationSetup_Bulk10000()
        {
            _BenchmarkName = nameof(Bulk10000);
            IterationSetup(TestData.CreateCustomers(Constants.N_10_000));
        }

        [IterationCleanup()]
        public void IterationCleanup()
        {
            BenchmarkMetadata.Instance.AddMetadata("FileSize", $"{(_Product.GetFileSizeBytes() / 1024).ToString("N0")} KB");
            BenchmarkMetadata.Instance.Save($"{_BenchmarkName}_{Product}");
            _Product.Cleanup();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_1_000)]
        public void Bulk1000()
        {
            _Product.Bulk();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_5_000)]
        public void Bulk5000()
        {
            _Product.Bulk();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_10_000)]
        public void Bulk10000()
        {
            _Product.Bulk();
        }
    }
}
