// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    internal static class SequenceNumberActionExtensions
    {
        public static string ToSerialString(this SequenceNumberAction value) => value switch
        {
            SequenceNumberAction.Max => "max",
            SequenceNumberAction.Update => "update",
            SequenceNumberAction.Increment => "increment",
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown SequenceNumberAction value.")
        };

        public static SequenceNumberAction ToSequenceNumberAction(this string value) => value switch
        {
            "max" => SequenceNumberAction.Max,
            "update" => SequenceNumberAction.Update,
            "increment" => SequenceNumberAction.Increment,
            _ => throw new ArgumentOutOfRangeException(nameof(value), value, "Unknown SequenceNumberAction value.")
        };
    }
}
