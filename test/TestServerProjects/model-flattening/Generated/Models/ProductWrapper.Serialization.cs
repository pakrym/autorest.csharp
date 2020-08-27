// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace model_flattening.Models
{
    public partial class ProductWrapper
    {
        internal static ProductWrapper DeserializeProductWrapper(JsonElement element)
        {
            Optional<string> value = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("property"))
                {
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        if (property0.NameEquals("value"))
                        {
                            value = property0.Value.GetString();
                            continue;
                        }
                    }
                    continue;
                }
            }
            return new ProductWrapper(value.Value);
        }
    }
}
