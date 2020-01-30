// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace body_complex.Models
{
    public partial class DictionaryWrapper : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (DefaultProgram != null)
            {
                writer.WritePropertyName("defaultProgram");
                writer.WriteStartObject();
                foreach (var item in DefaultProgram)
                {
                    writer.WritePropertyName(item.Key);
                    writer.WriteStringValue(item.Value);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
        internal static DictionaryWrapper DeserializeDictionaryWrapper(JsonElement element)
        {
            DictionaryWrapper result = new DictionaryWrapper();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("defaultProgram"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.DefaultProgram = new System.Collections.Generic.IDictionary<string, string>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        result.DefaultProgram.Add(property0.Name, property0.Value.GetString());
                    }
                    continue;
                }
            }
            return result;
        }
    }
}
