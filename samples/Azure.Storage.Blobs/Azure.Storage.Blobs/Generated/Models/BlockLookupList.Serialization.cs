// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlockLookupList : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "BlockList");
            if (Committed != null)
            {
                foreach (var item in Committed)
                {
                    writer.WriteStartElement("Committed");
                    writer.WriteValue(item);
                    writer.WriteEndElement();
                }
            }
            if (Uncommitted != null)
            {
                foreach (var item in Uncommitted)
                {
                    writer.WriteStartElement("Uncommitted");
                    writer.WriteValue(item);
                    writer.WriteEndElement();
                }
            }
            if (Latest != null)
            {
                foreach (var item in Latest)
                {
                    writer.WriteStartElement("Latest");
                    writer.WriteValue(item);
                    writer.WriteEndElement();
                }
            }
            writer.WriteEndElement();
        }
        internal static BlockLookupList DeserializeBlockLookupList(XElement element)
        {
            BlockLookupList result = default;
            result = new BlockLookupList(); result.Committed = new List<string>();
            foreach (var e in element.Elements("Committed"))
            {
                string value = default;
                value = (string)e;
                result.Committed.Add(value);
            }
            result.Uncommitted = new List<string>();
            foreach (var e0 in element.Elements("Uncommitted"))
            {
                string value = default;
                value = (string)e0;
                result.Uncommitted.Add(value);
            }
            result.Latest = new List<string>();
            foreach (var e1 in element.Elements("Latest"))
            {
                string value = default;
                value = (string)e1;
                result.Latest.Add(value);
            }
            return result;
        }
    }
}
