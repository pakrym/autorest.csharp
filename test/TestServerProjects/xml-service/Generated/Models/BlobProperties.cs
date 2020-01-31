// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

#nullable disable

using System;

namespace xml_service.Models
{
    /// <summary> Properties of a blob. </summary>
    public partial class BlobProperties
    {
        public DateTimeOffset LastModified { get; set; }
        public string Etag { get; set; }
        /// <summary> Size in bytes. </summary>
        public long? ContentLength { get; set; }
        public string ContentType { get; set; }
        public string ContentEncoding { get; set; }
        public string ContentLanguage { get; set; }
        public string ContentMd5 { get; set; }
        public string ContentDisposition { get; set; }
        public string CacheControl { get; set; }
        public int? BlobSequenceNumber { get; set; }
        public BlobType? BlobType { get; set; }
        public LeaseStatusType? LeaseStatus { get; set; }
        public LeaseStateType? LeaseState { get; set; }
        public LeaseDurationType? LeaseDuration { get; set; }
        public string CopyId { get; set; }
        public CopyStatusType? CopyStatus { get; set; }
        public string CopySource { get; set; }
        public string CopyProgress { get; set; }
        public DateTimeOffset? CopyCompletionTime { get; set; }
        public string CopyStatusDescription { get; set; }
        public bool? ServerEncrypted { get; set; }
        public bool? IncrementalCopy { get; set; }
        public string DestinationSnapshot { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
        public int? RemainingRetentionDays { get; set; }
        public AccessTier? AccessTier { get; set; }
        public bool? AccessTierInferred { get; set; }
        public ArchiveStatus? ArchiveStatus { get; set; }
    }
}
