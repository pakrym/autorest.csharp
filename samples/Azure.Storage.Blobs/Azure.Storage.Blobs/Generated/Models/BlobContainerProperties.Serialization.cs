// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobContainerProperties : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "BlobContainerProperties");
            writer.WriteStartElement("Last-Modified");
            writer.WriteValue(LastModified, "R");
            writer.WriteEndElement();
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
            if (PublicAccess != null)
            {
                writer.WriteStartElement("PublicAccess");
                writer.WriteValue(PublicAccess.Value.ToSerialString());
                writer.WriteEndElement();
            }
            if (HasImmutabilityPolicy != null)
            {
                writer.WriteStartElement("HasImmutabilityPolicy");
                writer.WriteValue(HasImmutabilityPolicy.Value);
                writer.WriteEndElement();
            }
            if (HasLegalHold != null)
            {
                writer.WriteStartElement("HasLegalHold");
                writer.WriteValue(HasLegalHold.Value);
                writer.WriteEndElement();
            }
            writer.WriteStartElement("Etag");
            writer.WriteValue(ETag);
            writer.WriteEndElement();
            if (Metadata != null)
            {
                foreach (var pair in Metadata)
                {
                    writer.WriteStartElement("!dictionary-item");
                    writer.WriteValue(pair.Value);
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
        internal static BlobContainerProperties DeserializeBlobContainerProperties(XElement element)
        {
            BlobContainerProperties result = default;
            result = new BlobContainerProperties(); DateTimeOffset value = default;
            var lastModified = element.Element("Last-Modified");
            if (lastModified != null)
            {
                value = lastModified.GetDateTimeOffsetValue("R");
            }
            result.LastModified = value;
            LeaseStatus? value0 = default;
            var leaseStatus = element.Element("LeaseStatus");
            if (leaseStatus != null)
            {
                value0 = leaseStatus.Value.ToLeaseStatus();
            }
            result.LeaseStatus = value0;
            LeaseState? value1 = default;
            var leaseState = element.Element("LeaseState");
            if (leaseState != null)
            {
                value1 = leaseState.Value.ToLeaseState();
            }
            result.LeaseState = value1;
            LeaseDurationType? value2 = default;
            var leaseDuration = element.Element("LeaseDuration");
            if (leaseDuration != null)
            {
                value2 = leaseDuration.Value.ToLeaseDurationType();
            }
            result.LeaseDuration = value2;
            PublicAccessType? value3 = default;
            var publicAccess = element.Element("PublicAccess");
            if (publicAccess != null)
            {
                value3 = publicAccess.Value.ToPublicAccessType();
            }
            result.PublicAccess = value3;
            bool? value4 = default;
            var hasImmutabilityPolicy = element.Element("HasImmutabilityPolicy");
            if (hasImmutabilityPolicy != null)
            {
                value4 = (bool?)hasImmutabilityPolicy;
            }
            result.HasImmutabilityPolicy = value4;
            bool? value5 = default;
            var hasLegalHold = element.Element("HasLegalHold");
            if (hasLegalHold != null)
            {
                value5 = (bool?)hasLegalHold;
            }
            result.HasLegalHold = value5;
            string value6 = default;
            var etag = element.Element("Etag");
            if (etag != null)
            {
                value6 = (string)etag;
            }
            result.ETag = value6;
            IDictionary<string, string>? value7 = default;
            var metadata = element.Element("Metadata");
            if (metadata != null)
            {
                value7 = new Dictionary<string, string>(); var elements = metadata.Elements();
                foreach (var e in elements)
                {
                    string value8 = default;
                    value8 = (string)e;
                    value7.Add(e.Name.LocalName, value8);
                }
            }
            result.Metadata = value7;
            return result;
        }
    }
}
