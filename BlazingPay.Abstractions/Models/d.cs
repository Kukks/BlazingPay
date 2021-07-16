using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BlazingPay.UI.Services
{
    class ListJsonConverterConverter<T, I> : JsonConverter<List<T>> where I : JsonConverter<T>
    {

        public override List<T> Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var converter = Activator.CreateInstance<I>();
            if (reader.TokenType != JsonTokenType.StartArray)
            {
                throw new JsonException();
            }

            var value = new List<T>();
            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndArray)
                {
                    return value;
                }

                value.Add(converter.Read(ref reader, typeof(T), options));

            }

            throw new JsonException();
        }

        public override void Write(Utf8JsonWriter writer, List<T> value, JsonSerializerOptions options)
        {
            if (value is null)
            {
                writer.WriteNullValue();
            }
            else
            {
                writer.WriteStartArray();

                var converter = Activator.CreateInstance<I>();
                foreach (T item in value)
                {
                    converter.Write(writer, item, options);
                }

                writer.WriteEndArray();
            }
        }
    }
}