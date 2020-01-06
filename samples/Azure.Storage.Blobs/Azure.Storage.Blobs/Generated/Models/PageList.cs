// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> the list of pages. </summary>
    public partial class PageList
    {
        /// <summary> MISSING·SCHEMA-DESCRIPTION-ARRAYSCHEMA. </summary>
        public ICollection<PageRange>? PageRange { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-ARRAYSCHEMA. </summary>
        public ICollection<ClearRange>? ClearRange { get; set; }
    }
}
