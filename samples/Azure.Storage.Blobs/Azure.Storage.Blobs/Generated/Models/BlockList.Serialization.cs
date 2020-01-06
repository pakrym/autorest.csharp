// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class BlockList : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "BlockList");
            if (CommittedBlocks != null)
            {
                writer.WriteStartElement("CommittedBlocks");
                foreach (var item in CommittedBlocks)
                {
                    writer.WriteObjectValue(item, "Block");
                }
                writer.WriteEndElement();
            }
            if (UncommittedBlocks != null)
            {
                writer.WriteStartElement("UncommittedBlocks");
                foreach (var item in UncommittedBlocks)
                {
                    writer.WriteObjectValue(item, "Block");
                }
                writer.WriteEndElement();
            }
            writer.WriteEndElement();
        }
        internal static BlockList DeserializeBlockList(XElement element)
        {
            BlockList result = default;
            result = new BlockList(); var committedBlocks = element.Element("CommittedBlocks");
            if (committedBlocks != null)
            {
                result.CommittedBlocks = new List<BlobBlock>();
                foreach (var e in committedBlocks.Elements("Block"))
                {
                    BlobBlock value = default;
                    value = BlobBlock.DeserializeBlobBlock(e);
                    result.CommittedBlocks.Add(value);
                }
            }
            var uncommittedBlocks = element.Element("UncommittedBlocks");
            if (uncommittedBlocks != null)
            {
                result.UncommittedBlocks = new List<BlobBlock>();
                foreach (var e in uncommittedBlocks.Elements("Block"))
                {
                    BlobBlock value = default;
                    value = BlobBlock.DeserializeBlobBlock(e);
                    result.UncommittedBlocks.Add(value);
                }
            }
            return result;
        }
    }
}
