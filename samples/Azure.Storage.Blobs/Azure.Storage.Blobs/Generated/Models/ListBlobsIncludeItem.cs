// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> MISSING·SCHEMA-DESCRIPTION-CHOICE. </summary>
    public enum ListBlobsIncludeItem
    {
        /// <summary> copy. </summary>
        Copy,
        /// <summary> deleted. </summary>
        Deleted,
        /// <summary> metadata. </summary>
        Metadata,
        /// <summary> snapshots. </summary>
        Snapshots,
        /// <summary> uncommittedblobs. </summary>
        Uncommittedblobs
    }
}
