// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> An Azure Storage container. </summary>
    public partial class BlobContainerItem
    {
        /// <summary> MISSING·SCHEMA-DESCRIPTION-STRING. </summary>
        public string Name { get; set; }
        /// <summary> Properties of a container. </summary>
        public BlobContainerProperties Properties { get; set; } = new BlobContainerProperties();
    }
}
