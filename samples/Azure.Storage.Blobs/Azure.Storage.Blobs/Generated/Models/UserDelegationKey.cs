// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> A user delegation key. </summary>
    public partial class UserDelegationKey
    {
        /// <summary> The Azure Active Directory object ID in GUID format. </summary>
        public string SignedOid { get; set; }
        /// <summary> The Azure Active Directory tenant ID in GUID format. </summary>
        public string SignedTid { get; set; }
        /// <summary> Abbreviation of the Azure Storage service that accepts the key. </summary>
        public string SignedService { get; set; }
        /// <summary> The service version that created the key. </summary>
        public string SignedVersion { get; set; }
        /// <summary> The key as a base64 string. </summary>
        public string Value { get; set; }
        /// <summary> The date-time the key expires. </summary>
        public DateTimeOffset SignedExpiresOn { get; set; }
        /// <summary> The date-time the key is active. </summary>
        public DateTimeOffset SignedStartsOn { get; set; }
    }
}
