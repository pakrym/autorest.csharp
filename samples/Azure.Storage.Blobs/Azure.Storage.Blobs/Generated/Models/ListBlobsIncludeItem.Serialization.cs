// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class ListBlobsIncludeItemExtensions
    {
        public static string ToSerialString(this ListBlobsIncludeItem value) => value switch
        {
            ListBlobsIncludeItem.Copy => "copy",
            ListBlobsIncludeItem.Deleted => "deleted",
            ListBlobsIncludeItem.Metadata => "metadata",
            ListBlobsIncludeItem.Snapshots => "snapshots",
            ListBlobsIncludeItem.Uncommittedblobs => "uncommittedblobs",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown ListBlobsIncludeItem value.")
        };

        public static ListBlobsIncludeItem ToListBlobsIncludeItem(this string value) => value switch
        {
            "copy" => ListBlobsIncludeItem.Copy,
            "deleted" => ListBlobsIncludeItem.Deleted,
            "metadata" => ListBlobsIncludeItem.Metadata,
            "snapshots" => ListBlobsIncludeItem.Snapshots,
            "uncommittedblobs" => ListBlobsIncludeItem.Uncommittedblobs,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown ListBlobsIncludeItem value.")
        };
    }
}
