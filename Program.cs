using BenchmarkDotNet.Running;
using System.Threading.Tasks;

namespace Stellar.Benchmarking
{
    public class Program
    {
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