// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> The status of the secondary location. </summary>
    public enum BlobGeoReplicationStatus
    {
        /// <summary> live. </summary>
        Live,
        /// <summary> bootstrap. </summary>
        Bootstrap,
        /// <summary> unavailable. </summary>
        Unavailable
    }
}
