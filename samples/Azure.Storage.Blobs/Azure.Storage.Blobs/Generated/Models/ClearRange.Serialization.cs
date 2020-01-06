// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class ClearRange : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "ClearRange");
            writer.WriteStartElement("Start");
            writer.WriteValue(Start);
            writer.WriteEndElement();
            writer.WriteStartElement("End");
            writer.WriteValue(End);
            writer.WriteEndElement();
            writer.WriteEndElement();
        }
        internal static ClearRange DeserializeClearRange(XElement element)
        {
            ClearRange result = default;
            result = new ClearRange(); long value = default;
            var start = element.Element("Start");
            if (start != null)
            {
                value = (long)start;
            }
            result.Start = value;
            long value0 = default;
            var end = element.Element("End");
            if (end != null)
            {
                value0 = (long)end;
            }
            result.End = value0;
            return result;
        }
    }
}
