// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class KeyInfo : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "KeyInfo");
            if (StartsOn != null)
            {
                writer.WriteStartElement("Start");
                writer.WriteValue(StartsOn);
                writer.WriteEndElement();
            }
            writer.WriteStartElement("Expiry");
            writer.WriteValue(ExpiresOn);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        internal static KeyInfo DeserializeKeyInfo(XElement element)
        {
            KeyInfo result = default;
            result = new KeyInfo(); string? value = default;
            var start = element.Element("Start");
            if (start != null)
            {
                value = (string?)start;
            }
            result.StartsOn = value;
            string value0 = default;
            var expiry = element.Element("Expiry");
            if (expiry != null)
            {
                value0 = (string)expiry;
            }
            result.ExpiresOn = value0;
            return result;
        }
    }
}
