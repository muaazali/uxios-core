using UnityEngine;
using Uxios.Core.Models;

namespace Uxios.Core.Logging
{
    public static class UxiosLogger
    {
        public static void LogRequest(UxiosRequest request)
        {
            Debug.Log($"<color=#4FC3F7><b>[Uxios] HTTP {request.Method}</b></color> <b>URL:</b> <color=#90CAF9>{request.Url}</color>\n" +
                      (request.Body != null ? $"<b>Body:</b> <color=#B3E5FC>{request.Body}</color>\n" : "") +
                      (request.Headers != null && request.Headers.Count > 0 ? $"<b>Headers:</b> <color=#B3E5FC>{FormatHeaders(request.Headers)}</color>\n" : ""));
        }

        public static void LogResponse<T>(UxiosResponse<T> response)
        {
            Debug.Log($"<color=#81C784><b>[Uxios] Response</b></color> <b>Status:</b> <color=#A5D6A7>{response.StatusCode}</color>\n" +
                      (response.RawBody != null ? $"<b>Body:</b> <color=#C8E6C9>{Truncate(response.RawBody, 1000)}</color>\n" : "") +
                      (response.Headers != null && response.Headers.Count > 0 ? $"<b>Headers:</b> <color=#C8E6C9>{FormatHeaders(response.Headers)}</color>\n" : ""));
        }

        public static void LogError(string message)
        {
            Debug.LogError($"<color=#E57373><b>[Uxios] Error:</b></color> {message}");
        }

        private static string FormatHeaders(System.Collections.Generic.Dictionary<string, string> headers)
        {
            System.Text.StringBuilder sb = new();
            foreach (var kv in headers)
                sb.Append($"{kv.Key}: {kv.Value}; ");
            return sb.ToString();
        }

        private static string Truncate(string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) return value;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength) + "... [truncated]";
        }
    }
}
