// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class ArchiveStatusExtensions
    {
        public static string ToSerialString(this ArchiveStatus value) => value switch
        {
            ArchiveStatus.RehydratePendingToHot => "rehydrate-pending-to-hot",
            ArchiveStatus.RehydratePendingToCool => "rehydrate-pending-to-cool",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown ArchiveStatus value.")
        };

        public static ArchiveStatus ToArchiveStatus(this string value) => value switch
        {
            "rehydrate-pending-to-hot" => ArchiveStatus.RehydratePendingToHot,
            "rehydrate-pending-to-cool" => ArchiveStatus.RehydratePendingToCool,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown ArchiveStatus value.")
        };
    }
}
