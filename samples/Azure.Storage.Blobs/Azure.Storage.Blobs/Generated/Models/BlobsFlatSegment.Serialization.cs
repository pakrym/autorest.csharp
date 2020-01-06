// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobsFlatSegment : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "EnumerationResults");
            writer.WriteStartAttribute("ServiceEndpoint");
            writer.WriteValue(ServiceEndpoint);
            writer.WriteEndAttribute();
            writer.WriteStartAttribute("ContainerName");
            writer.WriteValue(ContainerName);
            writer.WriteEndAttribute();
            if (Prefix != null)
            {
                writer.WriteStartElement("Prefix");
                writer.WriteValue(Prefix);
                writer.WriteEndElement();
            }
            if (Marker != null)
            {
                writer.WriteStartElement("Marker");
                writer.WriteValue(Marker);
                writer.WriteEndElement();
            }
            if (MaxResults != null)
            {
                writer.WriteStartElement("MaxResults");
                writer.WriteValue(MaxResults.Value);
                writer.WriteEndElement();
            }
            writer.WriteStartElement("NextMarker");
            writer.WriteValue(NextMarker);
            writer.WriteEndElement();
            if (BlobItems != null)
            {
                writer.WriteStartElement("Blobs");
                foreach (var item in BlobItems)
                {
                    writer.WriteObjectValue(item, "Blob");
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static BlobsFlatSegment DeserializeBlobsFlatSegment(XElement element)
        {
            BlobsFlatSegment result = default;
            result = new BlobsFlatSegment(); var serviceEndpoint = element.Attribute("ServiceEndpoint");
            if (serviceEndpoint != null)
            {
                result.ServiceEndpoint = (string)serviceEndpoint;
            }
            var containerName = element.Attribute("ContainerName");
            if (containerName != null)
            {
                result.ContainerName = (string)containerName;
            }
            string? value = default;
            var prefix = element.Element("Prefix");
            if (prefix != null)
            {
                value = (string?)prefix;
            }
            result.Prefix = value;
            string? value0 = default;
            var marker = element.Element("Marker");
            if (marker != null)
            {
                value0 = (string?)marker;
            }
            result.Marker = value0;
            int? value1 = default;
            var maxResults = element.Element("MaxResults");
            if (maxResults != null)
            {
                value1 = (int?)maxResults;
            }
            result.MaxResults = value1;
            string value2 = default;
            var nextMarker = element.Element("NextMarker");
            if (nextMarker != null)
            {
                value2 = (string)nextMarker;
            }
            result.NextMarker = value2;
            var blobs = element.Element("Blobs");
            if (blobs != null)
            {
                result.BlobItems = new List<BlobItem>();
                foreach (var e in blobs.Elements("Blob"))
                {
                    BlobItem value3 = default;
                    value3 = BlobItem.DeserializeBlobItem(e);
                    result.BlobItems.Add(value3);
                }
            }
            return result;
        }
    }
}
