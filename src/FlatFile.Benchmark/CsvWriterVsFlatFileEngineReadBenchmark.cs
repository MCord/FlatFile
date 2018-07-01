namespace FlatFile.Benchmark
{
    using System.IO;
    using System.Linq;
    using System.Text;
    using BenchmarkDotNet.Attributes;
    using BenchmarkDotNet.Attributes.Jobs;
    using BenchmarkDotNet.Engines;
    using BenchmarkDotNet.Running;
    using CsvHelper;
    using Delimited.Implementation;
    using Entities;
    using Mapping;
    using Xunit;

    [SimpleJob(RunStrategy.Monitoring, warmupCount: 1000, targetCount: 10000)]
    public class CsvWriterVsFlatFileEngineReadBenchmark
    {
        const string fileContent =
            @"String Column,Int Column,Guid Column,Custom Type Column
one,1,f96a1c66-4777-4642-86fa-703098065f5f,1|2|3
two,2,06776ed9-d33f-470f-bd3f-8db842356330,4|5|6
";

        [Benchmark]
        public void FlatFileEngineRead()
        {
            var layout = new FlatFileMappingForCustomObject();
            using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent)))
            {
                var factory = new DelimitedFileEngineFactory();

                var flatFile = factory.GetEngine(layout);

                var objects = flatFile.Read<CustomObject>(stream).ToArray();
            }
        }

        [Benchmark]
        private void CsvWriterRead()
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(fileContent)))
            using (var streamReader = new StreamReader(memoryStream))
            using (var reader = new CsvReader(streamReader))
            {
                reader.Configuration.RegisterClassMap<CsvHelperMappingForCustomObject>();

                var objects = reader.GetRecords<CustomObject>().ToArray();
            }
        }

        [Fact()]
        public void ReadAllRecordsWithMapping()
        {
            BenchmarkRunner.Run<CsvWriterVsFlatFileEngineReadBenchmark>();
        }
    }
}