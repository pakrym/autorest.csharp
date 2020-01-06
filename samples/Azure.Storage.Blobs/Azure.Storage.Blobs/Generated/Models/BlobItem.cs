// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> An Azure Storage blob. </summary>
    public partial class BlobItem
    {
        /// <summary> MISSING·SCHEMA-DESCRIPTION-STRING. </summary>
        public string Name { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-BOOLEAN. </summary>
        public bool Deleted { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-STRING. </summary>
        public string? Snapshot { get; set; }
        /// <summary> Properties of a blob. </summary>
        public BlobItemProperties Properties { get; set; } = new BlobItemProperties();
        /// <summary> MISSING·SCHEMA-DESCRIPTION-OBJECTSCHEMA. </summary>
        public BlobMetadata? Metadata { get; set; }
    }
}
