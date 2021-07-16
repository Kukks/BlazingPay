using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using BTCPayServer.Client;

namespace BlazingPay.Abstractions.Models
{
    public class PermissionJsonConverter : JsonConverter<Permission>
    {
        public override Permission Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.Null)
                return null;
            if (reader.TokenType != JsonTokenType.String)
                throw new FormatException("Type 'Permission' is expected to be a 'String'");
            if (reader.GetString() is { } s && Permission.TryParse(s, out var permission))
                return permission;
            throw new FormatException("Invalid 'Permission' String");
        }

        public override void Write(Utf8JsonWriter writer, Permission value, JsonSerializerOptions options)
        {
            if (value is { } v)
                writer.WriteStringValue(v.ToString());
        }

    }
}