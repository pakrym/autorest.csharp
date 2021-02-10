// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Text.Json;
using Azure.Core;

namespace Azure.AI.FormRecognizer.Models
{
    public partial class Model
    {
        internal static Model DeserializeModel(JsonElement element)
        {
            ModelInfo modelInfo = default;
            Optional<KeysResult> keys = default;
            Optional<TrainResult> trainResult = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("modelInfo"))
                {
                    modelInfo = ModelInfo.DeserializeModelInfo(property.Value);
                    continue;
                }
                if (property.NameEquals("keys"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    keys = KeysResult.DeserializeKeysResult(property.Value);
                    continue;
                }
                if (property.NameEquals("trainResult"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    trainResult = TrainResult.DeserializeTrainResult(property.Value);
                    continue;
                }
            }
            return new Model(modelInfo, keys.Value, trainResult.Value);
        }
    }
}