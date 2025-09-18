using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace KvizHub.SerializationConverter
{
    public class TimeSpanJsonConverter : JsonConverter<TimeSpan>
    {
        public override TimeSpan Read(ref Utf8JsonReader reader, Type targetType, JsonSerializerOptions options)
        {
            if (reader.TokenType != JsonTokenType.StartObject)
            {
                throw new JsonException("Expected object for TimeSpan deserialization.");
            }

            long days = 0, hours = 0, minutes = 0, seconds = 0, milliseconds = 0;

            while (reader.Read() && reader.TokenType != JsonTokenType.EndObject)
            {
                if (reader.TokenType == JsonTokenType.PropertyName)
                {
                    string propertyName = reader.GetString()?.Trim().ToLowerInvariant();
                    reader.Read();

                    switch (propertyName)
                    {
                        case "days": days = reader.GetInt64(); break;
                        case "hours": hours = reader.GetInt64(); break;
                        case "minutes": minutes = reader.GetInt64(); break;
                        case "seconds": seconds = reader.GetInt64(); break;
                        case "milliseconds": milliseconds = reader.GetInt64(); break;
                    }
                }
            }

            return new TimeSpan((int)days, (int)hours, (int)minutes, (int)seconds, (int)milliseconds);
        }

        public override void Write(Utf8JsonWriter writer, TimeSpan value, JsonSerializerOptions options)
        {
            writer.WriteStartObject();
            writer.WriteNumber("days", value.Days);
            writer.WriteNumber("hours", value.Hours);
            writer.WriteNumber("minutes", value.Minutes);
            writer.WriteNumber("seconds", value.Seconds);
            writer.WriteNumber("milliseconds", value.Milliseconds);
            writer.WriteEndObject();
        }
    }
}

