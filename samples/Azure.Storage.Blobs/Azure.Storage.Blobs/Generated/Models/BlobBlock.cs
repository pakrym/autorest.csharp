// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> Represents a single block in a block blob.  It describes the block&apos;s ID and size. </summary>
    public partial class BlobBlock
    {
        /// <summary> The base64 encoded block ID. </summary>
        public string Name { get; set; }
        /// <summary> The block size in bytes. </summary>
        public int Size { get; set; }
    }
}
