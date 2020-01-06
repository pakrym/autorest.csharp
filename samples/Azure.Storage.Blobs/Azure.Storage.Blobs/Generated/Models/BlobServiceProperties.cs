// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> Storage Service Properties. </summary>
    public partial class BlobServiceProperties
    {
        /// <summary> Azure Analytics Logging settings. </summary>
        public BlobAnalyticsLogging? Logging { get; set; }
        /// <summary> a summary of request statistics grouped by API in hour or minute aggregates for blobs. </summary>
        public BlobMetrics? HourMetrics { get; set; }
        /// <summary> a summary of request statistics grouped by API in hour or minute aggregates for blobs. </summary>
        public BlobMetrics? MinuteMetrics { get; set; }
        /// <summary> The set of CORS rules. </summary>
        public ICollection<BlobCorsRule>? Cors { get; set; }
        /// <summary> The default version to use for requests to the Blob service if an incoming request&apos;s version is not specified. Possible values include version 2008-10-27 and all more recent versions. </summary>
        public string? DefaultServiceVersion { get; set; }
        /// <summary> the retention policy which determines how long the associated data should persist. </summary>
        public BlobRetentionPolicy? DeleteRetentionPolicy { get; set; }
        /// <summary> The properties that enable an account to host a static website. </summary>
        public BlobStaticWebsite? StaticWebsite { get; set; }
    }
}
