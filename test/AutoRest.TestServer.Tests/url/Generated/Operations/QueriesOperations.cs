// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core;
using Azure.Core.Pipeline;
using url.Models.V100;

namespace url.Operations.V100
{
    public static class QueriesOperations
    {
        public static async ValueTask<Response> GetBooleanTrueAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetBooleanTrue");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/bool/true"));
                request.Uri.AppendQuery("boolQuery", "true".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetBooleanFalseAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetBooleanFalse");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/bool/false"));
                request.Uri.AppendQuery("boolQuery", "false".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetBooleanNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", bool? boolQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetBooleanNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/bool/null"));
                if (boolQuery != null)
                {
                    request.Uri.AppendQuery("boolQuery", boolQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetIntOneMillionAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetIntOneMillion");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/int/1000000"));
                request.Uri.AppendQuery("intQuery", "1000000".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetIntNegativeOneMillionAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetIntNegativeOneMillion");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/int/-1000000"));
                request.Uri.AppendQuery("intQuery", "-1000000".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetIntNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", int? intQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetIntNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/int/null"));
                if (intQuery != null)
                {
                    request.Uri.AppendQuery("intQuery", intQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetTenBillionAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetTenBillion");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/long/10000000000"));
                request.Uri.AppendQuery("longQuery", "10000000000".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetNegativeTenBillionAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetNegativeTenBillion");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/long/-10000000000"));
                request.Uri.AppendQuery("longQuery", "-10000000000".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> GetLongNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", int? longQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.GetLongNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/long/null"));
                if (longQuery != null)
                {
                    request.Uri.AppendQuery("longQuery", longQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> FloatScientificPositiveAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.FloatScientificPositive");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/float/1.034E+20"));
                request.Uri.AppendQuery("floatQuery", "103400000000000000000".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> FloatScientificNegativeAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.FloatScientificNegative");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/float/-1.034E-20"));
                request.Uri.AppendQuery("floatQuery", "-1.034e-20".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> FloatNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", double? floatQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.FloatNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/float/null"));
                if (floatQuery != null)
                {
                    request.Uri.AppendQuery("floatQuery", floatQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> DoubleDecimalPositiveAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.DoubleDecimalPositive");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/double/9999999.999"));
                request.Uri.AppendQuery("doubleQuery", "9999999.999".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> DoubleDecimalNegativeAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.DoubleDecimalNegative");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/double/-9999999.999"));
                request.Uri.AppendQuery("doubleQuery", "-9999999.999".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> DoubleNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", double? doubleQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.DoubleNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/double/null"));
                if (doubleQuery != null)
                {
                    request.Uri.AppendQuery("doubleQuery", doubleQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> StringUnicodeAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.StringUnicode");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/string/unicode/"));
                request.Uri.AppendQuery("stringQuery", "啊齄丂狛狜隣郎隣兀﨩".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> StringUrlEncodedAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.StringUrlEncoded");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/string/begin%21%2A%27%28%29%3B%3A%40%20%26%3D%2B%24%2C%2F%3F%23%5B%5Dend"));
                request.Uri.AppendQuery("stringQuery", "begin!*'();:@ &=+$,/?#[]end".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> StringEmptyAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.StringEmpty");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/string/empty"));
                request.Uri.AppendQuery("stringQuery", "".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> StringNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", string? stringQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.StringNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/string/null"));
                if (stringQuery != null)
                {
                    request.Uri.AppendQuery("stringQuery", stringQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> EnumValidAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", UriColor? enumQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.EnumValid");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/enum/green%20color"));
                if (enumQuery != null)
                {
                    request.Uri.AppendQuery("enumQuery", enumQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> EnumNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", UriColor? enumQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.EnumNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/enum/null"));
                if (enumQuery != null)
                {
                    request.Uri.AppendQuery("enumQuery", enumQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> ByteMultiByteAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", byte[]? byteQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.ByteMultiByte");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/byte/multibyte"));
                if (byteQuery != null)
                {
                    request.Uri.AppendQuery("byteQuery", byteQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> ByteEmptyAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.ByteEmpty");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/byte/empty"));
                request.Uri.AppendQuery("byteQuery", "".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> ByteNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", byte[]? byteQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.ByteNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/byte/null"));
                if (byteQuery != null)
                {
                    request.Uri.AppendQuery("byteQuery", byteQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> DateValidAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.DateValid");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/date/2012-01-01"));
                request.Uri.AppendQuery("dateQuery", "2012-01-01".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> DateNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", DateTime? dateQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.DateNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/date/null"));
                if (dateQuery != null)
                {
                    request.Uri.AppendQuery("dateQuery", dateQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> DateTimeValidAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.DateTimeValid");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/datetime/2012-01-01T01%3A01%3A01Z"));
                request.Uri.AppendQuery("dateTimeQuery", "2012-01-01T01:01:01Z".ToString()!);
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> DateTimeNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", DateTime? dateTimeQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.DateTimeNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/datetime/null"));
                if (dateTimeQuery != null)
                {
                    request.Uri.AppendQuery("dateTimeQuery", dateTimeQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> ArrayStringCsvValidAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", IEnumerable<string>? arrayQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.ArrayStringCsvValid");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/array/csv/string/valid"));
                if (arrayQuery != null)
                {
                    request.Uri.AppendQuery("arrayQuery", arrayQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> ArrayStringCsvNullAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", IEnumerable<string>? arrayQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.ArrayStringCsvNull");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/array/csv/string/null"));
                if (arrayQuery != null)
                {
                    request.Uri.AppendQuery("arrayQuery", arrayQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> ArrayStringCsvEmptyAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", IEnumerable<string>? arrayQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.ArrayStringCsvEmpty");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/array/csv/string/empty"));
                if (arrayQuery != null)
                {
                    request.Uri.AppendQuery("arrayQuery", arrayQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> ArrayStringSsvValidAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", IEnumerable<string>? arrayQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.ArrayStringSsvValid");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/array/ssv/string/valid"));
                if (arrayQuery != null)
                {
                    request.Uri.AppendQuery("arrayQuery", arrayQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> ArrayStringTsvValidAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", IEnumerable<string>? arrayQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.ArrayStringTsvValid");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/array/tsv/string/valid"));
                if (arrayQuery != null)
                {
                    request.Uri.AppendQuery("arrayQuery", arrayQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }

        public static async ValueTask<Response> ArrayStringPipesValidAsync(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000", IEnumerable<string>? arrayQuery = default, CancellationToken cancellationToken = default)
        {
            using var scope = clientDiagnostics.CreateScope("url.Operations.V100.ArrayStringPipesValid");
            scope.Start();
            try
            {
                var request = pipeline.CreateRequest();
                request.Method = RequestMethod.Get;
                request.Uri.Reset(new Uri($"{host}/queries/array/pipes/string/valid"));
                if (arrayQuery != null)
                {
                    request.Uri.AppendQuery("arrayQuery", arrayQuery.ToString()!);
                }
                var response = await pipeline.SendRequestAsync(request, cancellationToken).ConfigureAwait(false);
                cancellationToken.ThrowIfCancellationRequested();
                return response;
            }
            catch (Exception e)
            {
                scope.Failed(e);
                throw;
            }
        }
    }
}
