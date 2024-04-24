using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Validators;

namespace Stellar.Benchmarking
{
    public class BenchmarkConfiguration : ManualConfig
    {
        public BenchmarkConfiguration()
        {
            AddJob(Job.Default
                .WithLaunchCount(1)
                .WithWarmupCount(1)
                .WithIterationCount(3)
                .WithInvocationCount(1)
                .WithUnrollFactor(1));

            AddAnalyser(EnvironmentAnalyser.Default);
            AddAnalyser(OutliersAnalyser.Default);
            AddAnalyser(MinIterationTimeAnalyser.Default);
            AddAnalyser(MultimodalDistributionAnalyzer.Default);
            AddAnalyser(RuntimeErrorAnalyser.Default);
            AddAnalyser(ZeroMeasurementAnalyser.Default);
            AddAnalyser(BaselineCustomAnalyzer.Default);
            AddAnalyser(HideColumnsAnalyser.Default);

            AddValidator(BaselineValidator.FailOnError);
            AddValidator(SetupCleanupValidator.FailOnError);
#if !DEBUG
            AddValidator(JitOptimizationsValidator.FailOnError);
#endif
            AddValidator(RunModeValidator.FailOnError);
            AddValidator(GenericBenchmarksValidator.DontFailOnError);
            AddValidator(DeferredExecutionValidator.FailOnError);
            AddValidator(ParamsAllValuesValidator.FailOnError);
            AddValidator(ParamsValidator.FailOnError);

            AddLogger(ConsoleLogger.Default);

            AddColumn(TargetMethodColumn.Method);
            AddColumnProvider(DefaultColumnProviders.Params);
            AddColumn(StatisticColumn.OperationsPerSecond);
            AddColumn(new BenchmarkCustomColumn("FileSize", UnitType.Size));
            AddColumn(BaselineRatioColumn.RatioMean);

            AddExporter(DefaultExporters.Csv);
            AddExporter(DefaultExporters.Markdown);
            AddExporter(DefaultExporters.Plain);
        }
    }
}