// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class RehydratePriorityExtensions
    {
        public static string ToSerialString(this RehydratePriority value) => value switch
        {
            RehydratePriority.High => "High",
            RehydratePriority.Standard => "Standard",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown RehydratePriority value.")
        };

        public static RehydratePriority ToRehydratePriority(this string value) => value switch
        {
            "High" => RehydratePriority.High,
            "Standard" => RehydratePriority.Standard,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown RehydratePriority value.")
        };
    }
}
