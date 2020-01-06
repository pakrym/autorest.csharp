// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> Key information. </summary>
    public partial class KeyInfo
    {
        /// <summary> The date-time the key is active in ISO 8601 UTC time. </summary>
        public string? StartsOn { get; set; }
        /// <summary> The date-time the key expires in ISO 8601 UTC time. </summary>
        public string ExpiresOn { get; set; }
    }
}
