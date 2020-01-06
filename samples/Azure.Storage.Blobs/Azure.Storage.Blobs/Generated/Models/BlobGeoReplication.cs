// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> Geo-Replication information for the Secondary Storage Service. </summary>
    public partial class BlobGeoReplication
    {
        /// <summary> The status of the secondary location. </summary>
        public BlobGeoReplicationStatus Status { get; set; }
        /// <summary> A GMT date/time value, to the second. All primary writes preceding this value are guaranteed to be available for read operations at the secondary. Primary writes after this point in time may or may not be available for reads. </summary>
        public DateTimeOffset? LastSyncedOn { get; set; }
    }
}
