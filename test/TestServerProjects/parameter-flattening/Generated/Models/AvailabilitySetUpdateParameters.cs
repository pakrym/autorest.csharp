// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System.Collections.Generic;

namespace parameter_flattening.Models
{
    /// <summary> The AvailabilitySetUpdateParameters. </summary>
    public partial class AvailabilitySetUpdateParameters
    {
        /// <summary> A description about the set of tags. </summary>
        public IDictionary<string, string> Tags { get; set; } = new System.Collections.Generic.Dictionary<string, string>();
    }
}
