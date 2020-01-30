// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;

namespace CognitiveSearch.Models
{
    public partial class WebApiSkill : IUtf8JsonSerializable
    {
        void IUtf8JsonSerializable.Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("uri");
            writer.WriteStringValue(Uri);
            if (HttpHeaders != null)
            {
                writer.WritePropertyName("httpHeaders");
                writer.WriteStartObject();
                foreach (var item in HttpHeaders)
                {
                    writer.WritePropertyName(item.Key);
                    writer.WriteStringValue(item.Value);
                }
                writer.WriteEndObject();
            }
            if (HttpMethod != null)
            {
                writer.WritePropertyName("httpMethod");
                writer.WriteStringValue(HttpMethod);
            }
            if (Timeout != null)
            {
                writer.WritePropertyName("timeout");
                writer.WriteStringValue(Timeout.Value, "P");
            }
            if (BatchSize != null)
            {
                writer.WritePropertyName("batchSize");
                writer.WriteNumberValue(BatchSize.Value);
            }
            if (DegreeOfParallelism != null)
            {
                writer.WritePropertyName("degreeOfParallelism");
                writer.WriteNumberValue(DegreeOfParallelism.Value);
            }
            writer.WritePropertyName("@odata.type");
            writer.WriteStringValue(OdataType);
            if (Name != null)
            {
                writer.WritePropertyName("name");
                writer.WriteStringValue(Name);
            }
            if (Description != null)
            {
                writer.WritePropertyName("description");
                writer.WriteStringValue(Description);
            }
            if (Context != null)
            {
                writer.WritePropertyName("context");
                writer.WriteStringValue(Context);
            }
            writer.WritePropertyName("inputs");
            writer.WriteStartArray();
            foreach (var item in Inputs)
            {
                writer.WriteObjectValue(item);
            }
            writer.WriteEndArray();
            writer.WritePropertyName("outputs");
            writer.WriteStartArray();
            foreach (var item0 in Outputs)
            {
                writer.WriteObjectValue(item0);
            }
            writer.WriteEndArray();
            writer.WriteEndObject();
        }
        internal static WebApiSkill DeserializeWebApiSkill(JsonElement element)
        {
            WebApiSkill result = new WebApiSkill();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("uri"))
                {
                    result.Uri = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("httpHeaders"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.HttpHeaders = new System.Collections.Generic.IDictionary<string, string>();
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        result.HttpHeaders.Add(property0.Name, property0.Value.GetString());
                    }
                    continue;
                }
                if (property.NameEquals("httpMethod"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.HttpMethod = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("timeout"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.Timeout = property.Value.GetTimeSpan("P");
                    continue;
                }
                if (property.NameEquals("batchSize"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.BatchSize = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("degreeOfParallelism"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.DegreeOfParallelism = property.Value.GetInt32();
                    continue;
                }
                if (property.NameEquals("@odata.type"))
                {
                    result.OdataType = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("name"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.Name = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("description"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.Description = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("context"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        continue;
                    }
                    result.Context = property.Value.GetString();
                    continue;
                }
                if (property.NameEquals("inputs"))
                {
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        result.Inputs.Add(InputFieldMappingEntry.DeserializeInputFieldMappingEntry(item));
                    }
                    continue;
                }
                if (property.NameEquals("outputs"))
                {
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        result.Outputs.Add(OutputFieldMappingEntry.DeserializeOutputFieldMappingEntry(item));
                    }
                    continue;
                }
            }
            return result;
        }
    }
}
