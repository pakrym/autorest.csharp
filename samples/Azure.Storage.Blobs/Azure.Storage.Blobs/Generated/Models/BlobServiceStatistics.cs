// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> Statistics for the storage service. </summary>
    public partial class BlobServiceStatistics
    {
        /// <summary> Geo-Replication information for the Secondary Storage Service. </summary>
        public BlobGeoReplication? GeoReplication { get; set; }
    }
}
