// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> An Access policy. </summary>
    public partial class BlobAccessPolicy
    {
        /// <summary> the date-time the policy is active. </summary>
        public DateTimeOffset StartsOn { get; set; }
        /// <summary> the date-time the policy expires. </summary>
        public DateTimeOffset ExpiresOn { get; set; }
        /// <summary> the permissions for the acl policy. </summary>
        public string Permissions { get; set; }
    }
}
