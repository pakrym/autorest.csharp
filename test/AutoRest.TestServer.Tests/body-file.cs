﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Threading.Tasks;
using AutoRest.TestServer.Tests.Infrastructure;
using body_boolean;
using body_file;
using NUnit.Framework;

namespace AutoRest.TestServer.Tests
{
    public class BodyFileTests : TestServerTestBase
    {
        public BodyFileTests(TestServerVersion version) : base(version, "files") { }

        private string SamplePngPath = Path.Combine(TestServerV2.GetBaseDirectory(), "routes", "sample.png");

        [Test]
        public Task FileStreamEmpty() => Test(async (host, pipeline) =>
        {
            var result = await new FilesOperations(ClientDiagnostics, pipeline, host).GetEmptyFileAsync();
            Assert.AreEqual(0, await result.Value.ReadAsync(new byte[10]));
        });

        [Test]
        public Task FileStreamNonempty() => Test(async (host, pipeline) =>
        {
            var result = await new FilesOperations(ClientDiagnostics, pipeline, host).GetFileAsync();
            var memoryStream = new MemoryStream();
            await result.Value.CopyToAsync(memoryStream);

            CollectionAssert.AreEqual(File.ReadAllBytes(SamplePngPath), memoryStream.ToArray());
        });


        [Test]
        [IgnoreOnTestServer(TestServerVersion.V2, "No match")]
        public Task FileStreamVeryLarge() => Test(async (host, pipeline) =>
        {
            var result = await new FilesOperations(ClientDiagnostics, pipeline, host).GetFileLargeAsync();
            var buffer = new byte[2 * 1024 * 1024L];
            var stream = result.Value;
            long total = 0;
            var count = await stream.ReadAsync(buffer, 0, buffer.Length);
            while (count > 0)
            {
                total += count;
                count = await stream.ReadAsync(buffer, 0, buffer.Length);
            }

            Assert.AreEqual(3000 * 1024 * 1024L, total);
        });


    }
}
