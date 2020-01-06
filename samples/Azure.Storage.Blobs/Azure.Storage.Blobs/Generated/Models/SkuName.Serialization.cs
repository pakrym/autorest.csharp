// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class SkuNameExtensions
    {
        public static string ToSerialString(this SkuName value) => value switch
        {
            SkuName.StandardLRS => "Standard_LRS",
            SkuName.StandardGRS => "Standard_GRS",
            SkuName.StandardRAGRS => "Standard_RAGRS",
            SkuName.StandardZRS => "Standard_ZRS",
            SkuName.PremiumLRS => "Premium_LRS",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown SkuName value.")
        };

        public static SkuName ToSkuName(this string value) => value switch
        {
            "Standard_LRS" => SkuName.StandardLRS,
            "Standard_GRS" => SkuName.StandardGRS,
            "Standard_RAGRS" => SkuName.StandardRAGRS,
            "Standard_ZRS" => SkuName.StandardZRS,
            "Premium_LRS" => SkuName.PremiumLRS,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown SkuName value.")
        };
    }
}
