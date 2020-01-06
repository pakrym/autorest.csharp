// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobRetentionPolicy : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "RetentionPolicy");
            writer.WriteStartElement("Enabled");
            writer.WriteValue(Enabled);
            writer.WriteEndElement();
            if (Days != null)
            {
                writer.WriteStartElement("Days");
                writer.WriteValue(Days.Value);
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static BlobRetentionPolicy DeserializeBlobRetentionPolicy(XElement element)
        {
            BlobRetentionPolicy result = default;
            result = new BlobRetentionPolicy(); bool value = default;
            var enabled = element.Element("Enabled");
            if (enabled != null)
            {
                value = (bool)enabled;
            }
            result.Enabled = value;
            int? value0 = default;
            var days = element.Element("Days");
            if (days != null)
            {
                value0 = (int?)days;
            }
            result.Days = value0;
            return result;
        }
    }
}
