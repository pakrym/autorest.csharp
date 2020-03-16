// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;

namespace CognitiveServices.TextAnalytics.Models
{
    /// <summary> The SentimentResponse. </summary>
    public partial class SentimentResponse
    {
        /// <summary> Sentiment analysis per document. </summary>
        public IList<DocumentSentiment> Documents { get; internal set; } = new List<DocumentSentiment>();
        /// <summary> Errors by document id. </summary>
        public IList<DocumentError> Errors { get; internal set; } = new List<DocumentError>();
        /// <summary> if showStats=true was specified in the request this field will contain information about the request payload. </summary>
        public RequestStatistics Statistics { get; internal set; }
        /// <summary> This field indicates which model is used for scoring. </summary>
        public string ModelVersion { get; internal set; }
    }
}
