// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlobAccessPolicy : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "AccessPolicy");
            writer.WriteStartElement("Start");
            writer.WriteValue(StartsOn, "S");
            writer.WriteEndElement();
            writer.WriteStartElement("Expiry");
            writer.WriteValue(ExpiresOn, "S");
            writer.WriteEndElement();
            writer.WriteStartElement("Permission");
            writer.WriteValue(Permissions);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        internal static BlobAccessPolicy DeserializeBlobAccessPolicy(XElement element)
        {
            BlobAccessPolicy result = default;
            result = new BlobAccessPolicy(); DateTimeOffset value = default;
            var start = element.Element("Start");
            if (start != null)
            {
                value = start.GetDateTimeOffsetValue("S");
            }
            result.StartsOn = value;
            DateTimeOffset value0 = default;
            var expiry = element.Element("Expiry");
            if (expiry != null)
            {
                value0 = expiry.GetDateTimeOffsetValue("S");
            }
            result.ExpiresOn = value0;
            string value1 = default;
            var permission = element.Element("Permission");
            if (permission != null)
            {
                value1 = (string)permission;
            }
            result.Permissions = value1;
            return result;
        }
    }
}
