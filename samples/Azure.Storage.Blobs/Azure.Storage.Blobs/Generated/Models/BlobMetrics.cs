// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> a summary of request statistics grouped by API in hour or minute aggregates for blobs. </summary>
    public partial class BlobMetrics
    {
        /// <summary> The version of Storage Analytics to configure. </summary>
        public string? Version { get; set; }
        /// <summary> Indicates whether metrics are enabled for the Blob service. </summary>
        public bool Enabled { get; set; }
        /// <summary> the retention policy which determines how long the associated data should persist. </summary>
        public BlobRetentionPolicy? RetentionPolicy { get; set; }
        /// <summary> Indicates whether metrics should generate summary statistics for called API operations. </summary>
        public bool? IncludeApis { get; set; }
    }
}
