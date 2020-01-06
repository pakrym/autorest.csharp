// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> MISSING·SCHEMA-DESCRIPTION-CHOICE. </summary>
    public enum LeaseState
    {
        /// <summary> available. </summary>
        Available,
        /// <summary> leased. </summary>
        Leased,
        /// <summary> expired. </summary>
        Expired,
        /// <summary> breaking. </summary>
        Breaking,
        /// <summary> broken. </summary>
        Broken
    }
}
