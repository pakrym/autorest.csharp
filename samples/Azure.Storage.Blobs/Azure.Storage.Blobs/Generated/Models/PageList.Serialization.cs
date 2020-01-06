// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using Azure.Core;

namespace Azure.Storage.Blobs.Models.V20190202
{
    public partial class PageList : IUtf8JsonSerializable, IXmlSerializable
    {
        void IXmlSerializable.Write(XmlWriter writer, string nameHint)
        {
            writer.WriteStartElement(nameHint ?? "PageList");
            if (PageRange != null)
            {
                foreach (var item in PageRange)
                {
                    writer.WriteObjectValue(item, "PageRange");
                }
            }
            if (ClearRange != null)
            {
                foreach (var item in ClearRange)
                {
                    writer.WriteObjectValue(item, "ClearRange");
                }
            }
            writer.WriteEndElement();
        }
        internal static PageList DeserializePageList(XElement element)
        {
            PageList result = default;
            result = new PageList(); result.PageRange = new List<PageRange>();
            foreach (var e in element.Elements("PageRange"))
            {
                PageRange value = default;
                value = V20190202.PageRange.DeserializePageRange(e);
                result.PageRange.Add(value);
            }
            result.ClearRange = new List<ClearRange>();
            foreach (var e0 in element.Elements("ClearRange"))
            {
                ClearRange value = default;
                value = V20190202.ClearRange.DeserializeClearRange(e0);
                result.ClearRange.Add(value);
            }
            return result;
        }
    }
}
