// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> the retention policy which determines how long the associated data should persist. </summary>
    public partial class BlobRetentionPolicy
    {
        /// <summary> Indicates whether a retention policy is enabled for the storage service. </summary>
        public bool Enabled { get; set; }
        /// <summary> Indicates the number of days that metrics or logging or soft-deleted data should be retained. All data older than this value will be deleted. </summary>
        public int? Days { get; set; }
    }
}
