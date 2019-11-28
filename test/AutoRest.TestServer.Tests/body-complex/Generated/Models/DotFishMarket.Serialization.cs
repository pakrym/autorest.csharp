// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text.Json;

namespace body_complex.Models.V20160229
{
    public partial class DotFishMarket
    {
        internal void Serialize(Utf8JsonWriter writer, bool includeName = true)
        {
            if (includeName)
            {
                writer.WriteStartObject("DotFishMarket");
            }
            else
            {
                writer.WriteStartObject();
            }
            if (SampleSalmon != null)
            {
                SampleSalmon?.Serialize(writer, true);
            }
            if (Salmons != null)
            {
                writer.WriteStartArray("salmons");
                foreach (var item in Salmons)
                {
                    item?.Serialize(writer, true);
                }
                writer.WriteEndArray();
            }
            if (SampleFish != null)
            {
                SampleFish?.Serialize(writer, true);
            }
            if (Fishes != null)
            {
                writer.WriteStartArray("fishes");
                foreach (var item in Fishes)
                {
                    item?.Serialize(writer, true);
                }
                writer.WriteEndArray();
            }
            writer.WriteEndObject();
        }
        internal static DotFishMarket Deserialize(JsonElement element)
        {
            var result = new DotFishMarket();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("sampleSalmon"))
                {
                    result.SampleSalmon = DotSalmon.Deserialize(property.Value);
                    continue;
                }
                if (property.NameEquals("salmons"))
                {
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        result.Salmons.Add(DotSalmon.Deserialize(item));
                    }
                    continue;
                }
                if (property.NameEquals("sampleFish"))
                {
                    result.SampleFish = DotFish.Deserialize(property.Value);
                    continue;
                }
                if (property.NameEquals("fishes"))
                {
                    foreach (var item in property.Value.EnumerateArray())
                    {
                        result.Fishes.Add(DotFish.Deserialize(item));
                    }
                    continue;
                }
            }
            return result;
        }
    }
}
