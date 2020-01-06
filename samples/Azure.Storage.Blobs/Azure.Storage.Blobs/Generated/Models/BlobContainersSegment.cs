// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> An enumeration of containers. </summary>
    public partial class BlobContainersSegment
    {
        /// <summary> MISSING·SCHEMA-DESCRIPTION-STRING. </summary>
        public string ServiceEndpoint { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-STRING. </summary>
        public string? Prefix { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-STRING. </summary>
        public string? Marker { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-INTEGER. </summary>
        public int? MaxResults { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-STRING. </summary>
        public string NextMarker { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-ARRAYSCHEMA. </summary>
        public ICollection<BlobContainerItem>? BlobContainerItems { get; set; }
    }
}
