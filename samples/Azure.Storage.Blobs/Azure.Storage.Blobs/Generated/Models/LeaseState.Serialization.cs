// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class LeaseStateExtensions
    {
        public static string ToSerialString(this LeaseState value) => value switch
        {
            LeaseState.Available => "available",
            LeaseState.Leased => "leased",
            LeaseState.Expired => "expired",
            LeaseState.Breaking => "breaking",
            LeaseState.Broken => "broken",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown LeaseState value.")
        };

        public static LeaseState ToLeaseState(this string value) => value switch
        {
            "available" => LeaseState.Available,
            "leased" => LeaseState.Leased,
            "expired" => LeaseState.Expired,
            "breaking" => LeaseState.Breaking,
            "broken" => LeaseState.Broken,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown LeaseState value.")
        };
    }
}
