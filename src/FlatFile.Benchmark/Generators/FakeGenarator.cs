namespace FlatFile.Benchmark.Generators
{
    using System.Linq;
    using Bogus;
    using Entities;

    public class FakeGenarator
    {
        private readonly Faker<FixedSampleRecord> faker;
        private long stateValue;

        public FakeGenarator()
        {
            faker = new Bogus.Faker<FixedSampleRecord>()
                .StrictMode(true)
                .RuleFor(c => c.Actividad, f => f.Random.Number(100, 200))
                .RuleFor(c => c.Nombre, f => f.Random.String(1, 160))
                .RuleFor(c => c.Cuit, f => stateValue);

        }
        public FixedSampleRecord Generate(int next)
        {
            stateValue = next;

            return faker.Generate(1).First();
        }
    }
}