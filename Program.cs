using BenchmarkDotNet.Running;
using System;
using System.Threading.Tasks;

namespace Stellar.Benchmarking
{
    public class Program
    {
        // FastDB benchmark suites, runnable individually with: fastdb <name>
        private static readonly (string Name, Type Type)[] _FastDBSuites = new (string, Type)[]
        {
            ("insert", typeof(InsertBenchmark)),
            ("delete", typeof(DeleteBenchmark)),
            ("bulk", typeof(BulkBenchmark)),
            ("query", typeof(QueryBenchmark)),
            ("upsert", typeof(UpsertBenchmark)),
            ("performance", typeof(FastDBPerformanceBenchmark)),
            ("recordsize", typeof(FastDBRecordSizeBenchmark)),
            ("recordcount", typeof(FastDBRecordCountBenchmark)),
            ("load", typeof(FastDBLoadBenchmark)),
            ("churn", typeof(FastDBChurnBenchmark)),
            ("concurrentwrite", typeof(FastDBConcurrentWriteBenchmark)),
        };

        private static async Task Main(string[] args)
        {
            if (args.Length == 0 || args[0] == "benchmark")
            {
                // runs a BenchmarkDotNet test suite
                BenchmarkConfiguration configuration = new BenchmarkConfiguration();

                BenchmarkRunner.Run<InsertBenchmark>(configuration);
                BenchmarkRunner.Run<DeleteBenchmark>(configuration);
                BenchmarkRunner.Run<BulkBenchmark>(configuration);
                BenchmarkRunner.Run<QueryBenchmark>(configuration);
                BenchmarkRunner.Run<UpsertBenchmark>(configuration);

                BenchmarkRunner.Run<FastDBPerformanceBenchmark>(configuration);
                BenchmarkRunner.Run<FastDBRecordSizeBenchmark>(configuration);
                BenchmarkRunner.Run<FastDBRecordCountBenchmark>(configuration);
                BenchmarkRunner.Run<FastDBLoadBenchmark>(configuration);
                BenchmarkRunner.Run<FastDBChurnBenchmark>(configuration);
                BenchmarkRunner.Run<FastDBConcurrentWriteBenchmark>(configuration);
            }
            else if (args[0] == "fastdb")
            {
                // runs FastDB benchmarks only (skips LiteDB, SQLite and VistaDB); optionally a single suite
                ProductFactory.OnlyProduct = "FastDB";

                string suiteName = null;
                if (args.Length > 1)
                    suiteName = args[1].ToLowerInvariant();

                BenchmarkConfiguration configuration = new BenchmarkConfiguration();

                bool found = false;
                foreach ((string name, Type type) in _FastDBSuites)
                {
                    if (suiteName == null || suiteName == name)
                    {
                        found = true;
                        BenchmarkRunner.Run(type, configuration);
                    }
                }

                if (!found)
                    Console.WriteLine($"Unknown suite '{suiteName}'. Available: {string.Join(", ", Array.ConvertAll(_FastDBSuites, a => a.Name))}");
            }
            else if (args[0] == "features")
            {
                // tests and validates all features used in the benchmark test
                int runs = 1;
                int n = 1000;

                for (int i = 0; i < runs; i++)
                    await FastDBFeatures.Test(n);

                for (int i = 0; i < runs; i++)
                    await LiteDBFeatures.Test(n);

                for (int i = 0; i < runs; i++)
                    await SQLiteFeatures.Test(n);
#if(VistaDB)
                for (int i = 0; i < runs; i++)
                    await VistaDBFeatures.Test(n);
#endif
            }
            else if (args[0] == "concurrency")
            {
                // subject FastDB to a concurrency stress test
                await FastDBConcurrency.Test(writers: 500, readers: 500);
            }
        }
    }
}
