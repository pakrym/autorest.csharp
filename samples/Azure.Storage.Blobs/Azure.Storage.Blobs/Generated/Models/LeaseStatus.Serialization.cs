// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class LeaseStatusExtensions
    {
        public static string ToSerialString(this LeaseStatus value) => value switch
        {
            LeaseStatus.Locked => "locked",
            LeaseStatus.Unlocked => "unlocked",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown LeaseStatus value.")
        };

        public static LeaseStatus ToLeaseStatus(this string value) => value switch
        {
            "locked" => LeaseStatus.Locked,
            "unlocked" => LeaseStatus.Unlocked,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown LeaseStatus value.")
        };
    }
}
