namespace Company.Shorts.Blocks.Presentation.Api.Configuration
{
    using Newtonsoft.Json;
    using System;

    internal sealed class ValueTrimConverter : JsonConverter<string?>
    {
        public override string? ReadJson(
            JsonReader reader,
            Type objectType,
            string? existingValue,
            bool hasExistingValue,
            JsonSerializer serializer)
        {
            return (reader.Value as string)?.Trim();
        }

        public override void WriteJson(
            JsonWriter writer,
            string? value,
            JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}