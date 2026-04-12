using System.Threading;
using System.Threading.Tasks;
using Uxios.Core.Config;
using Uxios.Core.Models;

namespace Uxios.Core
{
    public static class UxiosApi
    {
        private static UxiosClient _defaultClient = new UxiosClient();

        public static Task<UxiosResponse<T>> Get<T>(string url, UxiosConfig config = null, CancellationToken cancellationToken = default)
        {
            var req = new UxiosRequest
            {
                Method = HttpMethod.Get,
                Url = url,
                CancellationToken = cancellationToken
            };
            return (config == null ? _defaultClient : new UxiosClient(config)).SendAsync<T>(req);
        }

        public static Task<UxiosResponse<T>> Post<T>(string url, object body, UxiosConfig config = null, CancellationToken cancellationToken = default)
        {
            var req = new UxiosRequest
            {
                Method = HttpMethod.Post,
                Url = url,
                Body = config?.Serializer?.Serialize(body) ?? _defaultClient.Config.Serializer.Serialize(body),
                CancellationToken = cancellationToken
            };
            return (config == null ? _defaultClient : new UxiosClient(config)).SendAsync<T>(req);
        }

        public static Task<UxiosResponse<T>> Put<T>(string url, object body, UxiosConfig config = null, CancellationToken cancellationToken = default)
        {
            var req = new UxiosRequest
            {
                Method = HttpMethod.Put,
                Url = url,
                Body = config?.Serializer?.Serialize(body) ?? _defaultClient.Config.Serializer.Serialize(body),
                CancellationToken = cancellationToken
            };
            return (config == null ? _defaultClient : new UxiosClient(config)).SendAsync<T>(req);
        }

        public static Task<UxiosResponse<T>> Delete<T>(string url, UxiosConfig config = null, CancellationToken cancellationToken = default)
        {
            var req = new UxiosRequest
            {
                Method = HttpMethod.Delete,
                Url = url,
                CancellationToken = cancellationToken
            };
            return (config == null ? _defaultClient : new UxiosClient(config)).SendAsync<T>(req);
        }

        public static UxiosClient Create(UxiosConfig config)
        {
            return new UxiosClient(config);
        }

        /// <summary>
        /// Send a file (multipart/form-data) with optional form fields.
        /// </summary>
        public static Task<UxiosResponse<T>> SendFile<T>(
            string url,
            byte[] fileData,
            string fileName,
            string fieldName = "file",
            string contentType = "application/octet-stream",
            System.Collections.Generic.Dictionary<string, string> formFields = null,
            HttpMethod method = HttpMethod.Post,
            UxiosConfig config = null,
            CancellationToken cancellationToken = default)
        {
            var file = new Uxios.Core.Models.UxiosFile
            {
                FieldName = fieldName,
                FileName = fileName,
                Data = fileData,
                ContentType = contentType
            };
            var req = new UxiosRequest
            {
                Method = method,
                Url = url,
                Files = new System.Collections.Generic.List<UxiosFile> { file },
                FormFields = formFields ?? new System.Collections.Generic.Dictionary<string, string>(),
                CancellationToken = cancellationToken
            };
            return SendAsync<T>(req, config);
        }

        /// <summary>
        /// Send a custom UxiosRequest (including multipart/form-data).
        /// </summary>
        public static Task<UxiosResponse<T>> SendAsync<T>(UxiosRequest request, UxiosConfig config = null)
        {
            return (config == null ? _defaultClient : new UxiosClient(config)).SendAsync<T>(request);
        }
    }
}