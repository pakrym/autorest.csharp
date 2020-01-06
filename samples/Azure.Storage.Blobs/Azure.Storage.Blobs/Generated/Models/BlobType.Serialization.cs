// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class BlobTypeExtensions
    {
        public static string ToSerialString(this BlobType value) => value switch
        {
            BlobType.Block => "BlockBlob",
            BlobType.Page => "PageBlob",
            BlobType.Append => "AppendBlob",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown BlobType value.")
        };

        public static BlobType ToBlobType(this string value) => value switch
        {
            "BlockBlob" => BlobType.Block,
            "PageBlob" => BlobType.Page,
            "AppendBlob" => BlobType.Append,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown BlobType value.")
        };
    }
}
