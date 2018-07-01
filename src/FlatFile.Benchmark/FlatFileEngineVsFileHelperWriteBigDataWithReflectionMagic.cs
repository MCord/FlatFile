namespace FlatFile.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Attributes.Jobs;
    using BenchmarkDotNet.Engines;

    [SimpleJob(RunStrategy.Monitoring, warmupCount: 1000, targetCount: 10000)]
    public class
        FlatFileEngineVsFileHelperWriteBigDataWithReflectionMagic : FlatFileEngineVsFileHelperWriteBigDataBenchMark
    {
        [GlobalSetup]
        public override void Setup()
        {
            //HyperTypeDescriptionProvider.Add(typeof (FixedSampleRecord));
            base.Setup();
        }
    }
}