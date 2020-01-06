// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class AccountKindExtensions
    {
        public static string ToSerialString(this AccountKind value) => value switch
        {
            AccountKind.Storage => "Storage",
            AccountKind.BlobStorage => "BlobStorage",
            AccountKind.StorageV2 => "StorageV2",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown AccountKind value.")
        };

        public static AccountKind ToAccountKind(this string value) => value switch
        {
            "Storage" => AccountKind.Storage,
            "BlobStorage" => AccountKind.BlobStorage,
            "StorageV2" => AccountKind.StorageV2,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown AccountKind value.")
        };
    }
}
