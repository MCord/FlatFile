namespace FlatFile.Benchmark
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Attributes.Columns;
    using BenchmarkDotNet.Attributes.Exporters;
    using BenchmarkDotNet.Attributes.Jobs;
    using BenchmarkDotNet.Running;
    using Entities;
    using FileHelpers;
    using FixedLength.Implementation;
    using FluentAssertions;
    using Mapping;
    using Xunit;

    [ClrJob(isBaseline: true), CoreJob, MonoJob]
    [RPlotExporter, RankColumn]
    public class FlatFileEngineVsFileHelperEngineReadStream
    {
        [Benchmark]
        public void FlatFileEngine()
        {
            var layout = new FixedSampleRecordLayout();
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(FlatFileVsFileHelpersBenchmarkData.FixedFileSample)))
            {
                var factory = new FixedLengthFileEngineFactory();

                var flatFile = factory.GetEngine(layout);

                var records = flatFile.Read<FixedSampleRecord>(stream).ToArray();

                records.Should().HaveCount(19);
            }
        }

        [Benchmark]
        private void FileHelperEngine()
        {
            var engine = new FileHelperEngine<FixedSampleRecord>();
            using (var stream = new StringReader(FlatFileVsFileHelpersBenchmarkData.FixedFileSample))
            {
                var records = engine.ReadStream(stream);
                records.Should().HaveCount(19);
            }
        }

        [Fact(Skip = "Too long for CI")]
        public void ReadOperationShouldBeQuick()
        {
            BenchmarkRunner.Run<FlatFileEngineVsFileHelperEngineReadStream>();
        }
    }
}