using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Coinsbit.Client.Client.Models.Converters
{
    class ResponseMessageConverter : JsonConverter<MessageType>
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(MessageType);
        }

        public override MessageType Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                return new MessageType
                {
                    Message = reader.GetString(),
                };
            }

            return new MessageType
            {
                Messages = JsonSerializer.Deserialize<List<List<string>>>(ref reader, options),
            };
        }

        public override void Write(Utf8JsonWriter writer, MessageType value, JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
