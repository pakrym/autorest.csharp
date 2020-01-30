// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace model_flattening.Models
{
    public partial class GenericUrl : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (GenericValue != null)
            {
                writer.WritePropertyName("generic_value");
                writer.WriteStringValue(GenericValue);
            }
            writer.WriteEndObject();
        }
        internal static GenericUrl DeserializeGenericUrl(JsonElement element)
        {
            GenericUrl result = new GenericUrl();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("generic_value"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.GenericValue = property.Value.GetString();
                    continue;
                }
            }
            return result;
        }
    }
}
