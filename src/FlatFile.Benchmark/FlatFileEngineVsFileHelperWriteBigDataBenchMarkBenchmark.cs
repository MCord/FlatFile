namespace FlatFile.Benchmark
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Attributes.Jobs;
    using BenchmarkDotNet.Engines;
    using Entities;
    using FileHelpers;
    using FixedLength.Implementation;
    using Generators;
    using Mapping;

    [SimpleJob(RunStrategy.Monitoring, warmupCount: 1000, targetCount: 10000)]
    public class FlatFileEngineVsFileHelperWriteBigDataBenchMark
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
    }
}