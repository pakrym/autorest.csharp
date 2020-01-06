// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class DeleteSnapshotsOptionExtensions
    {
        public static string ToSerialString(this DeleteSnapshotsOption value) => value switch
        {
            DeleteSnapshotsOption.IncludeSnapshots => "include",
            DeleteSnapshotsOption.OnlySnapshots => "only",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown DeleteSnapshotsOption value.")
        };

        public static DeleteSnapshotsOption ToDeleteSnapshotsOption(this string value) => value switch
        {
            "include" => DeleteSnapshotsOption.IncludeSnapshots,
            "only" => DeleteSnapshotsOption.OnlySnapshots,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown DeleteSnapshotsOption value.")
        };
    }
}
