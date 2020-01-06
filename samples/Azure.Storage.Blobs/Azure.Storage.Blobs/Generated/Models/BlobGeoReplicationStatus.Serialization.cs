// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class BlobGeoReplicationStatusExtensions
    {
        public static string ToSerialString(this BlobGeoReplicationStatus value) => value switch
        {
            BlobGeoReplicationStatus.Live => "live",
            BlobGeoReplicationStatus.Bootstrap => "bootstrap",
            BlobGeoReplicationStatus.Unavailable => "unavailable",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown BlobGeoReplicationStatus value.")
        };

        public static BlobGeoReplicationStatus ToBlobGeoReplicationStatus(this string value) => value switch
        {
            "live" => BlobGeoReplicationStatus.Live,
            "bootstrap" => BlobGeoReplicationStatus.Bootstrap,
            "unavailable" => BlobGeoReplicationStatus.Unavailable,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown BlobGeoReplicationStatus value.")
        };
    }
}
