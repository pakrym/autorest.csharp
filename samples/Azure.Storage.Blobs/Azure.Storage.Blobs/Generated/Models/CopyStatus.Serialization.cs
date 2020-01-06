// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class CopyStatusExtensions
    {
        public static string ToSerialString(this CopyStatus value) => value switch
        {
            CopyStatus.Pending => "pending",
            CopyStatus.Success => "success",
            CopyStatus.Aborted => "aborted",
            CopyStatus.Failed => "failed",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown CopyStatus value.")
        };

        public static CopyStatus ToCopyStatus(this string value) => value switch
        {
            "pending" => CopyStatus.Pending,
            "success" => CopyStatus.Success,
            "aborted" => CopyStatus.Aborted,
            "failed" => CopyStatus.Failed,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown CopyStatus value.")
        };
    }
}
