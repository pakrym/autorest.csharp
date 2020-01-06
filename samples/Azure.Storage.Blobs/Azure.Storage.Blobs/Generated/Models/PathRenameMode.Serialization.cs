// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class PathRenameModeExtensions
    {
        public static string ToSerialString(this PathRenameMode value) => value switch
        {
            PathRenameMode.Legacy => "legacy",
            PathRenameMode.Posix => "posix",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown PathRenameMode value.")
        };

        public static PathRenameMode ToPathRenameMode(this string value) => value switch
        {
            "legacy" => PathRenameMode.Legacy,
            "posix" => PathRenameMode.Posix,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown PathRenameMode value.")
        };
    }
}
