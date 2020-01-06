// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> signed identifier. </summary>
    public partial class BlobSignedIdentifier
    {
        /// <summary> a unique id. </summary>
        public string Id { get; set; }
        /// <summary> An Access policy. </summary>
        public BlobAccessPolicy AccessPolicy { get; set; } = new BlobAccessPolicy();
    }
}
