// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

// <auto-generated/>

#nullable disable

using System.Threading;
using System.Threading.Tasks;
using Azure;
using Azure.Core.Pipeline;

namespace body_number
{
    public partial class NumberClient
    {
        private readonly ClientDiagnostics clientDiagnostics;
        private readonly HttpPipeline pipeline;
        internal NumberRestClient RestClient { get; }
        /// <summary> Initializes a new instance of NumberClient. </summary>
        internal NumberClient(ClientDiagnostics clientDiagnostics, HttpPipeline pipeline, string host = "http://localhost:3000")
        {
            RestClient = new NumberRestClient(clientDiagnostics, pipeline, host);
            this.clientDiagnostics = clientDiagnostics;
            this.pipeline = pipeline;
        }
        /// <summary> Get null Number value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<float>> GetNullAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetNullAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get null Number value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<float> GetNull(CancellationToken cancellationToken = default)
        {
            return RestClient.GetNull(cancellationToken);
        }
        /// <summary> Get invalid float Number value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<float>> GetInvalidFloatAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetInvalidFloatAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get invalid float Number value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<float> GetInvalidFloat(CancellationToken cancellationToken = default)
        {
            return RestClient.GetInvalidFloat(cancellationToken);
        }
        /// <summary> Get invalid double Number value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<double>> GetInvalidDoubleAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetInvalidDoubleAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get invalid double Number value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<double> GetInvalidDouble(CancellationToken cancellationToken = default)
        {
            return RestClient.GetInvalidDouble(cancellationToken);
        }
        /// <summary> Get invalid decimal Number value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<decimal>> GetInvalidDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetInvalidDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get invalid decimal Number value. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<decimal> GetInvalidDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.GetInvalidDecimal(cancellationToken);
        }
        /// <summary> Put big float value 3.402823e+20. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutBigFloatAsync(float numberBody, CancellationToken cancellationToken = default)
        {
            return await RestClient.PutBigFloatAsync(numberBody, cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put big float value 3.402823e+20. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutBigFloat(float numberBody, CancellationToken cancellationToken = default)
        {
            return RestClient.PutBigFloat(numberBody, cancellationToken);
        }
        /// <summary> Get big float value 3.402823e+20. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<float>> GetBigFloatAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetBigFloatAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get big float value 3.402823e+20. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<float> GetBigFloat(CancellationToken cancellationToken = default)
        {
            return RestClient.GetBigFloat(cancellationToken);
        }
        /// <summary> Put big double value 2.5976931e+101. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutBigDoubleAsync(double numberBody, CancellationToken cancellationToken = default)
        {
            return await RestClient.PutBigDoubleAsync(numberBody, cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put big double value 2.5976931e+101. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutBigDouble(double numberBody, CancellationToken cancellationToken = default)
        {
            return RestClient.PutBigDouble(numberBody, cancellationToken);
        }
        /// <summary> Get big double value 2.5976931e+101. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<double>> GetBigDoubleAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetBigDoubleAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get big double value 2.5976931e+101. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<double> GetBigDouble(CancellationToken cancellationToken = default)
        {
            return RestClient.GetBigDouble(cancellationToken);
        }
        /// <summary> Put big double value 99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutBigDoublePositiveDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.PutBigDoublePositiveDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put big double value 99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutBigDoublePositiveDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.PutBigDoublePositiveDecimal(cancellationToken);
        }
        /// <summary> Get big double value 99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<double>> GetBigDoublePositiveDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetBigDoublePositiveDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get big double value 99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<double> GetBigDoublePositiveDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.GetBigDoublePositiveDecimal(cancellationToken);
        }
        /// <summary> Put big double value -99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutBigDoubleNegativeDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.PutBigDoubleNegativeDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put big double value -99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutBigDoubleNegativeDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.PutBigDoubleNegativeDecimal(cancellationToken);
        }
        /// <summary> Get big double value -99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<double>> GetBigDoubleNegativeDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetBigDoubleNegativeDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get big double value -99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<double> GetBigDoubleNegativeDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.GetBigDoubleNegativeDecimal(cancellationToken);
        }
        /// <summary> Put big decimal value 2.5976931e+101. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutBigDecimalAsync(decimal numberBody, CancellationToken cancellationToken = default)
        {
            return await RestClient.PutBigDecimalAsync(numberBody, cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put big decimal value 2.5976931e+101. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutBigDecimal(decimal numberBody, CancellationToken cancellationToken = default)
        {
            return RestClient.PutBigDecimal(numberBody, cancellationToken);
        }
        /// <summary> Get big decimal value 2.5976931e+101. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<decimal>> GetBigDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetBigDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get big decimal value 2.5976931e+101. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<decimal> GetBigDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.GetBigDecimal(cancellationToken);
        }
        /// <summary> Put big decimal value 99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutBigDecimalPositiveDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.PutBigDecimalPositiveDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put big decimal value 99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutBigDecimalPositiveDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.PutBigDecimalPositiveDecimal(cancellationToken);
        }
        /// <summary> Get big decimal value 99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<decimal>> GetBigDecimalPositiveDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetBigDecimalPositiveDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get big decimal value 99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<decimal> GetBigDecimalPositiveDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.GetBigDecimalPositiveDecimal(cancellationToken);
        }
        /// <summary> Put big decimal value -99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutBigDecimalNegativeDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.PutBigDecimalNegativeDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put big decimal value -99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutBigDecimalNegativeDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.PutBigDecimalNegativeDecimal(cancellationToken);
        }
        /// <summary> Get big decimal value -99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<decimal>> GetBigDecimalNegativeDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetBigDecimalNegativeDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get big decimal value -99999999.99. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<decimal> GetBigDecimalNegativeDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.GetBigDecimalNegativeDecimal(cancellationToken);
        }
        /// <summary> Put small float value 3.402823e-20. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutSmallFloatAsync(float numberBody, CancellationToken cancellationToken = default)
        {
            return await RestClient.PutSmallFloatAsync(numberBody, cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put small float value 3.402823e-20. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutSmallFloat(float numberBody, CancellationToken cancellationToken = default)
        {
            return RestClient.PutSmallFloat(numberBody, cancellationToken);
        }
        /// <summary> Get big double value 3.402823e-20. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<double>> GetSmallFloatAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetSmallFloatAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get big double value 3.402823e-20. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<double> GetSmallFloat(CancellationToken cancellationToken = default)
        {
            return RestClient.GetSmallFloat(cancellationToken);
        }
        /// <summary> Put small double value 2.5976931e-101. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutSmallDoubleAsync(double numberBody, CancellationToken cancellationToken = default)
        {
            return await RestClient.PutSmallDoubleAsync(numberBody, cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put small double value 2.5976931e-101. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutSmallDouble(double numberBody, CancellationToken cancellationToken = default)
        {
            return RestClient.PutSmallDouble(numberBody, cancellationToken);
        }
        /// <summary> Get big double value 2.5976931e-101. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<double>> GetSmallDoubleAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetSmallDoubleAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get big double value 2.5976931e-101. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<double> GetSmallDouble(CancellationToken cancellationToken = default)
        {
            return RestClient.GetSmallDouble(cancellationToken);
        }
        /// <summary> Put small decimal value 2.5976931e-101. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response> PutSmallDecimalAsync(decimal numberBody, CancellationToken cancellationToken = default)
        {
            return await RestClient.PutSmallDecimalAsync(numberBody, cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Put small decimal value 2.5976931e-101. </summary>
        /// <param name="numberBody"> The Number to use. </param>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response PutSmallDecimal(decimal numberBody, CancellationToken cancellationToken = default)
        {
            return RestClient.PutSmallDecimal(numberBody, cancellationToken);
        }
        /// <summary> Get small decimal value 2.5976931e-101. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual async ValueTask<Response<decimal>> GetSmallDecimalAsync(CancellationToken cancellationToken = default)
        {
            return await RestClient.GetSmallDecimalAsync(cancellationToken).ConfigureAwait(false);
        }
        /// <summary> Get small decimal value 2.5976931e-101. </summary>
        /// <param name="cancellationToken"> The cancellation token to use. </param>
        public virtual Response<decimal> GetSmallDecimal(CancellationToken cancellationToken = default)
        {
            return RestClient.GetSmallDecimal(cancellationToken);
        }
    }
}
