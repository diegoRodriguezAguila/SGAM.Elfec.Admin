using Newtonsoft.Json;
using SGAM.Elfec.Model.Exceptions;
using System;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SGAM.Elfec.DataAccess.WebServices
{
    public class MultipartUploader
    {
        public string FilePath { get; set; }
        private WebClient _webClient;
        public Uri Url { get; set; }
        public WebHeaderCollection Headers { get; set; }
        public event UploadProgressChangedEventHandler UploadProgressChanged;

        public MultipartUploader(string baseURL, string endpoint, string filePath) :
            this(new Uri(new Uri(baseURL), endpoint), filePath)
        {
        }

        public MultipartUploader(Uri url, string filePath)
        {
            Headers = new WebHeaderCollection();
            Url = url;
            FilePath = filePath;
            _webClient = new WebClient();
        }

        public async Task<TResp> UploadAsync<TResp>()
        {
            _webClient.Headers = this.Headers;
            try
            {
                if (UploadProgressChanged != null)
                    _webClient.UploadProgressChanged += UploadProgressChanged;
                var result = await _webClient.UploadFileTaskAsync(Url, FilePath);
                return JsonConvert.DeserializeObject<TResp>(Encoding.UTF8.GetString(result));
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    var response = ex.Response as HttpWebResponse;
                    var exc = await ApiMultipartException.CreateAsync(response, ex.Message);
                    throw exc;
                }
                throw new ServerConnectException();
            }
        }
    }
}
