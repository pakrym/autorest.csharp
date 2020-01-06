// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class PublicAccessTypeExtensions
    {
        public static string ToSerialString(this PublicAccessType value) => value switch
        {
            PublicAccessType.BlobContainer => "container",
            PublicAccessType.Blob => "blob",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown PublicAccessType value.")
        };

        public static PublicAccessType ToPublicAccessType(this string value) => value switch
        {
            "container" => PublicAccessType.BlobContainer,
            "blob" => PublicAccessType.Blob,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown PublicAccessType value.")
        };
    }
}
