// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;

namespace Azure.AI.FormRecognizer.Models
{
    /// <summary> Information about the extracted key or value in a key-value pair. </summary>
    public partial class KeyValueElement
    {
        /// <summary> The text content of the key or value. </summary>
        public string Text { get; internal set; }
        /// <summary> Bounding box of the key or value. </summary>
        public IList<float> BoundingBox { get; internal set; }
        /// <summary> When includeTextDetails is set to true, a list of references to the text elements constituting this key or value. </summary>
        public IList<string> Elements { get; internal set; }
    }
}
