// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.Storage.Blobs.Models.V20190202
{
    /// <summary> The service error response object. </summary>
    public partial class DataLakeStorageErrorError
    {
        /// <summary> The service error code. </summary>
        public string? Code { get; set; }
        /// <summary> The service error message. </summary>
        public string? Message { get; set; }
    }
}
