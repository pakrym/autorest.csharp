// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace CognitiveSearch.Models
{
    public partial class IndexingParameters : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (BatchSize != null)
            {
                writer.WritePropertyName("batchSize");
                writer.WriteNumberValue(BatchSize.Value);
            }
            if (MaxFailedItems != null)
            {
                writer.WritePropertyName("maxFailedItems");
                writer.WriteNumberValue(MaxFailedItems.Value);
            }
            if (MaxFailedItemsPerBatch != null)
            {
                writer.WritePropertyName("maxFailedItemsPerBatch");
                writer.WriteNumberValue(MaxFailedItemsPerBatch.Value);
            }
            if (Base64EncodeKeys != null)
            {
                writer.WritePropertyName("base64EncodeKeys");
                writer.WriteBooleanValue(Base64EncodeKeys.Value);
            }
            if (Configuration != null)
            {
                writer.WritePropertyName("configuration");
                writer.WriteStartObject();
                foreach (var item in Configuration)
                {
                    writer.WritePropertyName(item.Key);
                    writer.WriteObjectValue(item.Value);
                }
                writer.WriteEndObject();
            }
            writer.WriteEndObject();
        }
        internal static IndexingParameters DeserializeIndexingParameters(JsonElement element)
        {
            IndexingParameters result = new IndexingParameters();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("batchSize"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.BatchSize = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("maxFailedItems"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.MaxFailedItems = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("maxFailedItemsPerBatch"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.MaxFailedItemsPerBatch = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("base64EncodeKeys"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.Base64EncodeKeys = property.Value.GetBoolean();
                    continue;
                }
                if (property.NameEquals("configuration"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.Configuration = new System.Collections.Generic.IDictionary<string, object>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        result.Configuration.Add(property0.Name, property0.Value.GetObject());
                    }
                    continue;
                }
            }
            return result;
        }
    }
}
