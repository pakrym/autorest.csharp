// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Collections.Generic;

namespace CognitiveServices.TextAnalytics.Models
{
    /// <summary> The DocumentLinkedEntities. </summary>
    public partial class DocumentLinkedEntities
    {
        /// <summary> Unique, non-empty document identifier. </summary>
        public string Id { get; set; }
        /// <summary> Recognized well-known entities in the document. </summary>
        public ICollection<LinkedEntity> Entities { get; set; } = new System.Collections.Generic.List<CognitiveServices.TextAnalytics.Models.LinkedEntity>();
        /// <summary> if showStats=true was specified in the request this field will contain information about the document payload. </summary>
        public DocumentStatistics Statistics { get; set; }
    }
}
