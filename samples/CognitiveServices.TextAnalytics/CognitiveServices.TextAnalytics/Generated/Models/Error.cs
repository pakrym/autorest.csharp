// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Collections.Generic;

namespace CognitiveServices.TextAnalytics.Models
{
    /// <summary> The Error. </summary>
    public partial class Error
    {
        /// <summary> Error code. </summary>
        public ErrorCode Code { get; set; }
        /// <summary> Error message. </summary>
        public string Message { get; set; }
        /// <summary> Error target. </summary>
        public string Target { get; set; }
        public InnerError Innererror { get; set; }
        /// <summary> Details about specific errors that led to this reported error. </summary>
        public ICollection<Error> Details { get; set; }
    }
}
