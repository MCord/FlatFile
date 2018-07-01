namespace FlatFile.Benchmark
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Attributes.Columns;
    using BenchmarkDotNet.Attributes.Exporters;
    using BenchmarkDotNet.Attributes.Jobs;
    using BenchmarkDotNet.Running;
    using Entities;
    using FileHelpers;
    using FixedLength.Implementation;
    using Generators;
    using Mapping;
    using Xunit;

    [ClrJob(isBaseline: true), CoreJob, MonoJob]
    [RPlotExporter, RankColumn]
    public class FlatFileEngineVsFileHelperWriteBigData
    {
        private IEnumerable<FixedSampleRecord> sampleRecords;

        [GlobalSetup]
        public virtual void Setup()
        {
            var genarator = new FakeGenarator();
            sampleRecords = Enumerable.Range(0, 100000).Select(genarator.Generate).ToArray();
        }
        [Benchmark]
        public void FlatFileEngine()
        {
            var layout = new FixedSampleRecordLayout();
            using (var stream = new MemoryStream())
            {
                var factory = new FixedLengthFileEngineFactory();

                var flatFile = factory.GetEngine(layout);

                flatFile.Write(stream, sampleRecords);
            }
        }

        [Benchmark]
        private void FileHelperEngine()
        {
            var engine = new FileHelperEngine<FixedSampleRecord>();
            using (var stream = new MemoryStream())
            using (var streamWriter = new StreamWriter(stream))
            {
                engine.WriteStream(streamWriter, sampleRecords);
            }
        }

        [Fact(Skip = "Too long for CI")]
        public void ReadOperationShouldBeQuick()
        {
            BenchmarkRunner.Run(GetType());
        }
    }
}