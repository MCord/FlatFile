namespace FlatFile.Benchmark.Converters
{
    using System;
    using CsvHelper;
    using CsvHelper.Configuration;
    using CsvHelperTypeConversion = CsvHelper.TypeConversion;

    public class CsvHelperTypeConverterForCustomType : CsvHelperTypeConversion.ITypeConverter
    {
        private readonly FlatFileTypeConverterForCustomType converter;

        public CsvHelperTypeConverterForCustomType()
        {
            converter = new FlatFileTypeConverterForCustomType();
        }

        public string ConvertToString(CsvHelperTypeConversion.TypeConverterOptions options, object value)
        {
            return converter.ConvertToString(value);
        }

        public object ConvertFromString(CsvHelperTypeConversion.TypeConverterOptions options, string text)
        {
            return converter.ConvertFromString(text);
        }

        public bool CanConvertFrom(Type type)
        {
            return converter.CanConvertFrom(type);
        }

        public bool CanConvertTo(Type type)
        {
            return converter.CanConvertTo(type);
        }

        public string ConvertToString(object value, IWriterRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }

        public object ConvertFromString(string text, IReaderRow row, MemberMapData memberMapData)
        {
            throw new NotImplementedException();
        }
    }
}
