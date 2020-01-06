// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobItemProperties : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "Properties");
            if (LastModified != null)
            {
                writer.WriteStartElement("Last-Modified");
                writer.WriteValue(LastModified.Value, "R");
                writer.WriteEndElement();
            }
            if (ContentLength != null)
            {
                writer.WriteStartElement("Content-Length");
                writer.WriteValue(ContentLength.Value);
                writer.WriteEndElement();
            }
            if (ContentType != null)
            {
                writer.WriteStartElement("Content-Type");
                writer.WriteValue(ContentType);
                writer.WriteEndElement();
            }
            if (ContentEncoding != null)
            {
                writer.WriteStartElement("Content-Encoding");
                writer.WriteValue(ContentEncoding);
                writer.WriteEndElement();
            }
            if (ContentLanguage != null)
            {
                writer.WriteStartElement("Content-Language");
                writer.WriteValue(ContentLanguage);
                writer.WriteEndElement();
            }
            if (ContentMD5 != null)
            {
                writer.WriteStartElement("Content-MD5");
                writer.WriteValue(ContentMD5);
                writer.WriteEndElement();
            }
            if (ContentDisposition != null)
            {
                writer.WriteStartElement("Content-Disposition");
                writer.WriteValue(ContentDisposition);
                writer.WriteEndElement();
            }
            if (CacheControl != null)
            {
                writer.WriteStartElement("Cache-Control");
                writer.WriteValue(CacheControl);
                writer.WriteEndElement();
            }
            if (BlobSequenceNumber != null)
            {
                writer.WriteStartElement("x-ms-blob-sequence-number");
                writer.WriteValue(BlobSequenceNumber.Value);
                writer.WriteEndElement();
            }
            if (BlobType != null)
            {
                writer.WriteStartElement("BlobType");
                writer.WriteValue(BlobType.Value.ToSerialString());
                writer.WriteEndElement();
            }
            if (LeaseStatus != null)
            {
                writer.WriteStartElement("LeaseStatus");
                writer.WriteValue(LeaseStatus.Value.ToSerialString());
                writer.WriteEndElement();
            }
            if (LeaseState != null)
            {
                writer.WriteStartElement("LeaseState");
                writer.WriteValue(LeaseState.Value.ToSerialString());
                writer.WriteEndElement();
            }
            if (LeaseDuration != null)
            {
                writer.WriteStartElement("LeaseDuration");
                writer.WriteValue(LeaseDuration.Value.ToSerialString());
                writer.WriteEndElement();
            }
            if (CopyId != null)
            {
                writer.WriteStartElement("CopyId");
                writer.WriteValue(CopyId);
                writer.WriteEndElement();
            }
            if (CopyStatus != null)
            {
                writer.WriteStartElement("CopyStatus");
                writer.WriteValue(CopyStatus.Value.ToSerialString());
                writer.WriteEndElement();
            }
            if (CopySource != null)
            {
                writer.WriteStartElement("CopySource");
                writer.WriteValue(CopySource);
                writer.WriteEndElement();
            }
            if (CopyProgress != null)
            {
                writer.WriteStartElement("CopyProgress");
                writer.WriteValue(CopyProgress);
                writer.WriteEndElement();
            }
            if (CopyStatusDescription != null)
            {
                writer.WriteStartElement("CopyStatusDescription");
                writer.WriteValue(CopyStatusDescription);
                writer.WriteEndElement();
            }
            if (ServerEncrypted != null)
            {
                writer.WriteStartElement("ServerEncrypted");
                writer.WriteValue(ServerEncrypted.Value);
                writer.WriteEndElement();
            }
            if (IncrementalCopy != null)
            {
                writer.WriteStartElement("IncrementalCopy");
                writer.WriteValue(IncrementalCopy.Value);
                writer.WriteEndElement();
            }
            if (DestinationSnapshot != null)
            {
                writer.WriteStartElement("DestinationSnapshot");
                writer.WriteValue(DestinationSnapshot);
                writer.WriteEndElement();
            }
            if (RemainingRetentionDays != null)
            {
                writer.WriteStartElement("RemainingRetentionDays");
                writer.WriteValue(RemainingRetentionDays.Value);
                writer.WriteEndElement();
            }
            if (AccessTier != null)
            {
                writer.WriteStartElement("AccessTier");
                writer.WriteValue(AccessTier.Value.ToString());
                writer.WriteEndElement();
            }
            writer.WriteStartElement("AccessTierInferred");
            writer.WriteValue(AccessTierInferred);
            writer.WriteEndElement();
            if (ArchiveStatus != null)
            {
                writer.WriteStartElement("ArchiveStatus");
                writer.WriteValue(ArchiveStatus.Value.ToSerialString());
                writer.WriteEndElement();
            }
            if (CustomerProvidedKeySha256 != null)
            {
                writer.WriteStartElement("CustomerProvidedKeySha256");
                writer.WriteValue(CustomerProvidedKeySha256);
                writer.WriteEndElement();
            }
            if (ETag != null)
            {
                writer.WriteStartElement("Etag");
                writer.WriteValue(ETag);
                writer.WriteEndElement();
            }
            if (CreatedOn != null)
            {
                writer.WriteStartElement("Creation-Time");
                writer.WriteValue(CreatedOn.Value, "R");
                writer.WriteEndElement();
            }
            if (CopyCompletedOn != null)
            {
                writer.WriteStartElement("CopyCompletionTime");
                writer.WriteValue(CopyCompletedOn.Value, "R");
                writer.WriteEndElement();
            }
            if (DeletedOn != null)
            {
                writer.WriteStartElement("DeletedTime");
                writer.WriteValue(DeletedOn.Value, "R");
                writer.WriteEndElement();
            }
            if (AccessTierChangedOn != null)
            {
                writer.WriteStartElement("AccessTierChangeTime");
                writer.WriteValue(AccessTierChangedOn.Value, "R");
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static BlobItemProperties DeserializeBlobItemProperties(XElement element)
        {
            BlobItemProperties result = default;
            result = new BlobItemProperties(); DateTimeOffset? value = default;
            var lastModified = element.Element("Last-Modified");
            if (lastModified != null)
            {
                value = lastModified.GetDateTimeOffsetValue("R");
            }
            result.LastModified = value;
            long? value0 = default;
            var contentLength = element.Element("Content-Length");
            if (contentLength != null)
            {
                value0 = (long?)contentLength;
            }
            result.ContentLength = value0;
            string? value1 = default;
            var contentType = element.Element("Content-Type");
            if (contentType != null)
            {
                value1 = (string?)contentType;
            }
            result.ContentType = value1;
            string? value2 = default;
            var contentEncoding = element.Element("Content-Encoding");
            if (contentEncoding != null)
            {
                value2 = (string?)contentEncoding;
            }
            result.ContentEncoding = value2;
            string? value3 = default;
            var contentLanguage = element.Element("Content-Language");
            if (contentLanguage != null)
            {
                value3 = (string?)contentLanguage;
            }
            result.ContentLanguage = value3;
            byte[]? value4 = default;
            var contentMD5 = element.Element("Content-MD5");
            if (contentMD5 != null)
            {
                value4 = contentMD5.GetBytesFromBase64(null);
            }
            result.ContentMD5 = value4;
            string? value5 = default;
            var contentDisposition = element.Element("Content-Disposition");
            if (contentDisposition != null)
            {
                value5 = (string?)contentDisposition;
            }
            result.ContentDisposition = value5;
            string? value6 = default;
            var cacheControl = element.Element("Cache-Control");
            if (cacheControl != null)
            {
                value6 = (string?)cacheControl;
            }
            result.CacheControl = value6;
            long? value7 = default;
            var xMsBlobSequenceNumber = element.Element("x-ms-blob-sequence-number");
            if (xMsBlobSequenceNumber != null)
            {
                value7 = (long?)xMsBlobSequenceNumber;
            }
            result.BlobSequenceNumber = value7;
            BlobType? value8 = default;
            var blobType = element.Element("BlobType");
            if (blobType != null)
            {
                value8 = blobType.Value.ToBlobType();
            }
            result.BlobType = value8;
            LeaseStatus? value9 = default;
            var leaseStatus = element.Element("LeaseStatus");
            if (leaseStatus != null)
            {
                value9 = leaseStatus.Value.ToLeaseStatus();
            }
            result.LeaseStatus = value9;
            LeaseState? value10 = default;
            var leaseState = element.Element("LeaseState");
            if (leaseState != null)
            {
                value10 = leaseState.Value.ToLeaseState();
            }
            result.LeaseState = value10;
            LeaseDurationType? value11 = default;
            var leaseDuration = element.Element("LeaseDuration");
            if (leaseDuration != null)
            {
                value11 = leaseDuration.Value.ToLeaseDurationType();
            }
            result.LeaseDuration = value11;
            string? value12 = default;
            var copyId = element.Element("CopyId");
            if (copyId != null)
            {
                value12 = (string?)copyId;
            }
            result.CopyId = value12;
            CopyStatus? value13 = default;
            var copyStatus = element.Element("CopyStatus");
            if (copyStatus != null)
            {
                value13 = copyStatus.Value.ToCopyStatus();
            }
            result.CopyStatus = value13;
            Uri? value14 = default;
            var copySource = element.Element("CopySource");
            if (copySource != null)
            {
                value14 = copySource.(null);
            }
            result.CopySource = value14;
            string? value15 = default;
            var copyProgress = element.Element("CopyProgress");
            if (copyProgress != null)
            {
                value15 = (string?)copyProgress;
            }
            result.CopyProgress = value15;
            string? value16 = default;
            var copyStatusDescription = element.Element("CopyStatusDescription");
            if (copyStatusDescription != null)
            {
                value16 = (string?)copyStatusDescription;
            }
            result.CopyStatusDescription = value16;
            bool? value17 = default;
            var serverEncrypted = element.Element("ServerEncrypted");
            if (serverEncrypted != null)
            {
                value17 = (bool?)serverEncrypted;
            }
            result.ServerEncrypted = value17;
            bool? value18 = default;
            var incrementalCopy = element.Element("IncrementalCopy");
            if (incrementalCopy != null)
            {
                value18 = (bool?)incrementalCopy;
            }
            result.IncrementalCopy = value18;
            string? value19 = default;
            var destinationSnapshot = element.Element("DestinationSnapshot");
            if (destinationSnapshot != null)
            {
                value19 = (string?)destinationSnapshot;
            }
            result.DestinationSnapshot = value19;
            int? value20 = default;
            var remainingRetentionDays = element.Element("RemainingRetentionDays");
            if (remainingRetentionDays != null)
            {
                value20 = (int?)remainingRetentionDays;
            }
            result.RemainingRetentionDays = value20;
            AccessTier? value21 = default;
            var accessTier = element.Element("AccessTier");
            if (accessTier != null)
            {
                value21 = new AccessTier(accessTier.Value);
            }
            result.AccessTier = value21;
            bool value22 = default;
            var accessTierInferred = element.Element("AccessTierInferred");
            if (accessTierInferred != null)
            {
                value22 = (bool)accessTierInferred;
            }
            result.AccessTierInferred = value22;
            ArchiveStatus? value23 = default;
            var archiveStatus = element.Element("ArchiveStatus");
            if (archiveStatus != null)
            {
                value23 = archiveStatus.Value.ToArchiveStatus();
            }
            result.ArchiveStatus = value23;
            string? value24 = default;
            var customerProvidedKeySha256 = element.Element("CustomerProvidedKeySha256");
            if (customerProvidedKeySha256 != null)
            {
                value24 = (string?)customerProvidedKeySha256;
            }
            result.CustomerProvidedKeySha256 = value24;
            string? value25 = default;
            var etag = element.Element("Etag");
            if (etag != null)
            {
                value25 = (string?)etag;
            }
            result.ETag = value25;
            DateTimeOffset? value26 = default;
            var creationTime = element.Element("Creation-Time");
            if (creationTime != null)
            {
                value26 = creationTime.GetDateTimeOffsetValue("R");
            }
            result.CreatedOn = value26;
            DateTimeOffset? value27 = default;
            var copyCompletionTime = element.Element("CopyCompletionTime");
            if (copyCompletionTime != null)
            {
                value27 = copyCompletionTime.GetDateTimeOffsetValue("R");
            }
            result.CopyCompletedOn = value27;
            DateTimeOffset? value28 = default;
            var deletedTime = element.Element("DeletedTime");
            if (deletedTime != null)
            {
                value28 = deletedTime.GetDateTimeOffsetValue("R");
            }
            result.DeletedOn = value28;
            DateTimeOffset? value29 = default;
            var accessTierChangeTime = element.Element("AccessTierChangeTime");
            if (accessTierChangeTime != null)
            {
                value29 = accessTierChangeTime.GetDateTimeOffsetValue("R");
            }
            result.AccessTierChangedOn = value29;
            return result;
        }
    }
}
