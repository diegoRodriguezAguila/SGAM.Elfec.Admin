using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.Model.Exceptions
{
    public class ApiMultipartException : Exception
    {
        /// <summary>
        /// Gets the HTTP status code received
        /// </summary>
        public HttpStatusCode StatusCode { get; private set; }

        /// <summary>
        /// Gets the ReasonPhrase associated with the response
        /// </summary>
        public string ReasonPhrase { get; private set; }

        /// <summary>
        /// Gets the headers associated with the response
        /// </summary>
        public WebHeaderCollection Headers { get; private set; }

        /// <summary>
        /// Gets the content associated with the response, if any
        /// </summary>
        public string Content { get; private set; }

        /// <summary>
        /// Gets a value indicating whether any content is associated with the response
        /// </summary>
        public bool HasContent
        {
            get { return !String.IsNullOrWhiteSpace(this.Content); }
        }

        internal ApiMultipartException(
            HttpStatusCode statusCode,
            string reasonPhrase,
            WebHeaderCollection headers,
            string content) : base(String.Format("Response status code does not indicate success: {0} ({1}).", (int)statusCode, reasonPhrase))
        {
            this.StatusCode = statusCode;
            this.ReasonPhrase = reasonPhrase;
            this.Headers = headers;
            this.Content = content;
        }
        public static async Task<ApiMultipartException> CreateAsync(HttpWebResponse response, string message)
        {
            if (response.ContentLength == 0)
                return new ApiMultipartException(response.StatusCode, message, response.Headers, null);
            string contentString = null;

            try
            {
                using (var stream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
                    {
                        contentString = await reader.ReadToEndAsync().ConfigureAwait(false);
                    }
                }
            }
            catch
            { } // Don't want to hide the original exception with a new one

            return new ApiMultipartException(response.StatusCode, message, response.Headers, contentString);
        }
    }
}
