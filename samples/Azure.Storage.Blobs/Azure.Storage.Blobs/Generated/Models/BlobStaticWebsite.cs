// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> The properties that enable an account to host a static website. </summary>
    public partial class BlobStaticWebsite
    {
        /// <summary> Indicates whether this account is hosting a static website. </summary>
        public bool Enabled { get; set; }
        /// <summary> The default name of the index page under each directory. </summary>
        public string? IndexDocument { get; set; }
        /// <summary> The absolute path of the custom 404 page. </summary>
        public string? ErrorDocument404Path { get; set; }
    }
}
