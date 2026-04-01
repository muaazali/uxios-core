using Uxios.Core.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine.Networking;
using Uxios.Core.Config;
using Uxios.Core.Exceptions;
using Uxios.Core.Models;
using Uxios.Core.Plugins;
using Uxios.Core.Internal;

namespace Uxios.Core
{
    public class UxiosClient
    {
        public UxiosConfig Config { get; }

        public UxiosClient(UxiosConfig config = null)
        {
            Config = config ?? new UxiosConfig();
        }

        public async Task<UxiosResponse<T>> SendAsync<T>(UxiosRequest request)
        {
            UxiosLogger.LogRequest(request);
            // Run plugins before request
            foreach (var plugin in Config.Plugins)
                await plugin.OnBeforeRequest(request);

            var tcs = new TaskCompletionSource<UxiosResponse<T>>();
            var cts = CancellationTokenSource.CreateLinkedTokenSource(request.CancellationToken);

            UnityWebRequest uwr = new UnityWebRequest(Config.BaseUrl + request.Url, request.Method.ToString().ToUpper());
            uwr.downloadHandler = new DownloadHandlerBuffer();

            foreach (var header in Config.DefaultHeaders)
                uwr.SetRequestHeader(header.Key, header.Value);
            foreach (var header in request.Headers)
                uwr.SetRequestHeader(header.Key, header.Value);

            if (!string.IsNullOrEmpty(request.Body))
            {
                var bodyRaw = System.Text.Encoding.UTF8.GetBytes(request.Body);
                uwr.uploadHandler = new UploadHandlerRaw(bodyRaw);
                uwr.SetRequestHeader("Content-Type", "application/json");
            }

            var op = uwr.SendWebRequest();
            cts.Token.Register(() => uwr.Abort());

            UxiosDispatcher.Instance.RunCoroutine(WaitForRequest(op, uwr, tcs, cts.Token));

            var response = await tcs.Task;

            // Run plugins after response
            foreach (var plugin in Config.Plugins)
                await plugin.OnAfterResponse(response);

            if (response.StatusCode < 200 || response.StatusCode >= 300)
            {
                UxiosLogger.LogError($"HTTP Error: {response.StatusCode}\nBody: {response.RawBody}");
                throw new UxiosException($"HTTP Error: {response.StatusCode}", response);
            }

            UxiosLogger.LogResponse(response);

            return response;
        }

        private System.Collections.IEnumerator WaitForRequest<T>(UnityWebRequestAsyncOperation op, UnityWebRequest uwr, TaskCompletionSource<UxiosResponse<T>> tcs, CancellationToken token)
        {
            yield return op;
            var resp = new UxiosResponse<T>
            {
                StatusCode = (int)uwr.responseCode,
                Headers = uwr.GetResponseHeaders() ?? new Dictionary<string, string>(),
                RawBody = uwr.downloadHandler.text
            };
            if (!token.IsCancellationRequested && string.IsNullOrEmpty(uwr.error))
            {
                try
                {
                    resp.Data = Config.Serializer.Deserialize<T>(resp.RawBody);
                }
                catch
                {
                    resp.Data = default;
                }
                tcs.TrySetResult(resp);
            }
            else
            {
                tcs.TrySetResult(resp);
            }
        }
    }
}