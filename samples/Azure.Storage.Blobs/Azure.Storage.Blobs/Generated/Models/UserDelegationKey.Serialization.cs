// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class UserDelegationKey : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "UserDelegationKey");
            writer.WriteStartElement("SignedOid");
            writer.WriteValue(SignedOid);
            writer.WriteEndElement();
            writer.WriteStartElement("SignedTid");
            writer.WriteValue(SignedTid);
            writer.WriteEndElement();
            writer.WriteStartElement("SignedService");
            writer.WriteValue(SignedService);
            writer.WriteEndElement();
            writer.WriteStartElement("SignedVersion");
            writer.WriteValue(SignedVersion);
            writer.WriteEndElement();
            writer.WriteStartElement("Value");
            writer.WriteValue(Value);
            writer.WriteEndElement();
            writer.WriteStartElement("SignedExpiry");
            writer.WriteValue(SignedExpiresOn, "S");
            writer.WriteEndElement();
            writer.WriteStartElement("SignedStart");
            writer.WriteValue(SignedStartsOn, "S");
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        internal static UserDelegationKey DeserializeUserDelegationKey(XElement element)
        {
            UserDelegationKey result = default;
            result = new UserDelegationKey(); string value = default;
            var signedOid = element.Element("SignedOid");
            if (signedOid != null)
            {
                value = (string)signedOid;
            }
            result.SignedOid = value;
            string value0 = default;
            var signedTid = element.Element("SignedTid");
            if (signedTid != null)
            {
                value0 = (string)signedTid;
            }
            result.SignedTid = value0;
            string value1 = default;
            var signedService = element.Element("SignedService");
            if (signedService != null)
            {
                value1 = (string)signedService;
            }
            result.SignedService = value1;
            string value2 = default;
            var signedVersion = element.Element("SignedVersion");
            if (signedVersion != null)
            {
                value2 = (string)signedVersion;
            }
            result.SignedVersion = value2;
            string value3 = default;
            var value4 = element.Element("Value");
            if (value4 != null)
            {
                value3 = (string)value4;
            }
            result.Value = value3;
            DateTimeOffset value5 = default;
            var signedExpiry = element.Element("SignedExpiry");
            if (signedExpiry != null)
            {
                value5 = signedExpiry.GetDateTimeOffsetValue("S");
            }
            result.SignedExpiresOn = value5;
            DateTimeOffset value6 = default;
            var signedStart = element.Element("SignedStart");
            if (signedStart != null)
            {
                value6 = signedStart.GetDateTimeOffsetValue("S");
            }
            result.SignedStartsOn = value6;
            return result;
        }
    }
}
