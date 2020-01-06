// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> Properties of a container. </summary>
    public partial class BlobContainerProperties
    {
        /// <summary> MISSING·SCHEMA-DESCRIPTION-DATETIME. </summary>
        public DateTimeOffset LastModified { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-CHOICE. </summary>
        public LeaseStatus? LeaseStatus { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-CHOICE. </summary>
        public LeaseState? LeaseState { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-CHOICE. </summary>
        public LeaseDurationType? LeaseDuration { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-CHOICE. </summary>
        public PublicAccessType? PublicAccess { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-BOOLEAN. </summary>
        public bool? HasImmutabilityPolicy { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-BOOLEAN. </summary>
        public bool? HasLegalHold { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-STRING. </summary>
        public string ETag { get; set; }
        /// <summary> Dictionary of &lt;components·schemas·blobmetadata·additionalproperties&gt;. </summary>
        public IDictionary<string, string>? Metadata { get; set; }
    }
}
