// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class BlockListTypeExtensions
    {
        public static string ToSerialString(this BlockListType value) => value switch
        {
            BlockListType.Committed => "committed",
            BlockListType.Uncommitted => "uncommitted",
            BlockListType.All => "all",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown BlockListType value.")
        };

        public static BlockListType ToBlockListType(this string value) => value switch
        {
            "committed" => BlockListType.Committed,
            "uncommitted" => BlockListType.Uncommitted,
            "all" => BlockListType.All,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown BlockListType value.")
        };
    }
}
