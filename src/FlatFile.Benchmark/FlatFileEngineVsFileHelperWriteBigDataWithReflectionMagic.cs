namespace FlatFile.Benchmark
{
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Attributes.Columns;
    using BenchmarkDotNet.Attributes.Exporters;
    using BenchmarkDotNet.Attributes.Jobs;
    using BenchmarkDotNet.Running;
    using Entities;
    using Xunit;

    [ClrJob(isBaseline: true), CoreJob, MonoJob]
    [RPlotExporter, RankColumn]
    public class FlatFileEngineVsFileHelperWriteBigDataWithReflectionMagic : FlatFileEngineVsFileHelperWriteBigData
    {

        [GlobalSetup]
        public override void Setup()
        {
            //HyperTypeDescriptionProvider.Add(typeof (FixedSampleRecord));
            base.Setup();
        }
       

        [Fact(Skip = "Too long for CI")]
        public void ReadOperationShouldBeQuickWithReflectionMagic()
        {
            BenchmarkRunner.Run<FlatFileEngineVsFileHelperWriteStream>();
        }
    }
}