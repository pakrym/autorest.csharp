// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> A list of block IDs split between the committed block list, in the uncommitted block list, or in the uncommitted block list first and then in the committed block list. </summary>
    public partial class BlockLookupList
    {
        /// <summary> MISSING·SCHEMA-DESCRIPTION-ARRAYSCHEMA. </summary>
        public ICollection<string>? Committed { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-ARRAYSCHEMA. </summary>
        public ICollection<string>? Uncommitted { get; set; }
        /// <summary> MISSING·SCHEMA-DESCRIPTION-ARRAYSCHEMA. </summary>
        public ICollection<string>? Latest { get; set; }
    }
}
