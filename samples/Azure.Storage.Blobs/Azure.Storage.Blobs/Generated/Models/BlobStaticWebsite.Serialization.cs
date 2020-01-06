// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobStaticWebsite : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "StaticWebsite");
            writer.WriteStartElement("Enabled");
            writer.WriteValue(Enabled);
            writer.WriteEndElement();
            if (IndexDocument != null)
            {
                writer.WriteStartElement("IndexDocument");
                writer.WriteValue(IndexDocument);
                writer.WriteEndElement();
            }
            if (ErrorDocument404Path != null)
            {
                writer.WriteStartElement("ErrorDocument404Path");
                writer.WriteValue(ErrorDocument404Path);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static BlobStaticWebsite DeserializeBlobStaticWebsite(XElement element)
        {
            BlobStaticWebsite result = default;
            result = new BlobStaticWebsite(); bool value = default;
            var enabled = element.Element("Enabled");
            if (enabled != null)
            {
                value = (bool)enabled;
            }
            result.Enabled = value;
            string? value0 = default;
            var indexDocument = element.Element("IndexDocument");
            if (indexDocument != null)
            {
                value0 = (string?)indexDocument;
            }
            result.IndexDocument = value0;
            string? value1 = default;
            var errorDocument404Path = element.Element("ErrorDocument404Path");
            if (errorDocument404Path != null)
            {
                value1 = (string?)errorDocument404Path;
            }
            result.ErrorDocument404Path = value1;
            return result;
        }
    }
}
