// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Collections.Generic;
using System.Xml.Linq;
using Azure.Core;

namespace xml_service.Models
{
    public partial class Blobs
    {
        internal static Blobs DeserializeBlobs(XElement element)
        {
            IList<BlobPrefix> blobPrefix = default;
            IList<Blob> blob = default;
            var array = new List<BlobPrefix>();
            foreach (var e in element.Elements("BlobPrefix"))
            {
                array.Add(Models.BlobPrefix.DeserializeBlobPrefix(e));
            }
            blobPrefix = array;
            var array0 = new List<Blob>();
            foreach (var e0 in element.Elements("Blob"))
            {
                array0.Add(Models.Blob.DeserializeBlob(e0));
            }
            blob = array0;
            return new Blobs(blobPrefix, blob);
        }
    }
}
