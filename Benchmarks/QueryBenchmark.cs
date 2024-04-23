using BenchmarkDotNet.Attributes;
using Stellar.Collections;
using System;
using System.Collections.Generic;

namespace Stellar.Benchmarking
{
    public class QueryBenchmark
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

        [IterationSetup(Target = nameof(Query1000))]
        public void IterationSetup_Query1000()
        {
            _BenchmarkName = nameof(Query1000);
            IterationSetup(TestData.CreateCustomers(Constants.N_1_000));
        }

        [IterationSetup(Target = nameof(Query5000))]
        public void IterationSetup_Query5000()
        {
            _BenchmarkName = nameof(Query5000);
            IterationSetup(TestData.CreateCustomers(Constants.N_5_000));
        }

        [IterationSetup(Target = nameof(Query10000))]
        public void IterationSetup_Query10000()
        {
            _BenchmarkName = nameof(Query10000);
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
        public void Query1000()
        {
            _Product.Query();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_5_000)]
        public void Query5000()
        {
            _Product.Query();
        }

        [Benchmark(OperationsPerInvoke = Constants.N_10_000)]
        public void Query10000()
        {
            _Product.Query();
        }
    }
}
