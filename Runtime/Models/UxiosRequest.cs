using System;
using System.Collections.Generic;
using System.Threading;

namespace Uxios.Core.Models
{
    public class UxiosRequest
    {
        public HttpMethod Method { get; set; }
        public string Url { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new();
        public string Body { get; set; }
        public CancellationToken CancellationToken { get; set; } = CancellationToken.None;

        // Multipart/form-data support
        public List<UxiosFile> Files { get; set; } = new();
        public Dictionary<string, string> FormFields { get; set; } = new();
        public bool IsMultipart => Files.Count > 0 || FormFields.Count > 0;
    }
}