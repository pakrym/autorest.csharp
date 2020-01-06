// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class LeaseDurationTypeExtensions
    {
        public static string ToSerialString(this LeaseDurationType value) => value switch
        {
            LeaseDurationType.Infinite => "infinite",
            LeaseDurationType.Fixed => "fixed",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown LeaseDurationType value.")
        };

        public static LeaseDurationType ToLeaseDurationType(this string value) => value switch
        {
            "infinite" => LeaseDurationType.Infinite,
            "fixed" => LeaseDurationType.Fixed,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown LeaseDurationType value.")
        };
    }
}
