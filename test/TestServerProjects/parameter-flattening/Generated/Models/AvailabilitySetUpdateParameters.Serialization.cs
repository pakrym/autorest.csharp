// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace parameter_flattening.Models
{
    public partial class AvailabilitySetUpdateParameters : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("tags");
            writer.WriteStartObject();
            foreach (var item in Tags)
            {
                writer.WritePropertyName(item.Key);
                writer.WriteStringValue(item.Value);
            }
            writer.WriteEndObject();
            writer.WriteEndObject();
        }
        internal static AvailabilitySetUpdateParameters DeserializeAvailabilitySetUpdateParameters(JsonElement element)
        {
            AvailabilitySetUpdateParameters result = new AvailabilitySetUpdateParameters();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("tags"))
                {
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        result.Tags.Add(property0.Name, property0.Value.GetString());
                    }
                    continue;
                }
            }
            return result;
        }
    }
}
