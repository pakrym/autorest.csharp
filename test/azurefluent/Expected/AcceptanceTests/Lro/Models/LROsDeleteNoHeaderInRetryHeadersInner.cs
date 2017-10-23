// <auto-generated>
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See License.txt in the project root for
// license information.
//
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Fixtures.Azure.Fluent.AcceptanceTestsLro.Models
{
    using Newtonsoft.Json;
    using System.Linq;

    /// <summary>
    /// Defines headers for deleteNoHeaderInRetry operation.
    /// </summary>
    public partial class LROsDeleteNoHeaderInRetryHeadersInner
    {
        /// <summary>
        /// Initializes a new instance of the
        /// LROsDeleteNoHeaderInRetryHeadersInner class.
        /// </summary>
        public LROsDeleteNoHeaderInRetryHeadersInner()
        {
            CustomInit();
        }

        /// <summary>
        /// An initialization method that performs custom operations like setting defaults
        /// </summary>
        partial void CustomInit();

        /// <summary>
        /// Gets or sets location to poll for result status: will be set to
        /// /lro/put/noheader/202/204/operationresults
        /// </summary>
        [JsonProperty(PropertyName = "Location")]
        public string Location { get; set; }

    }
}
