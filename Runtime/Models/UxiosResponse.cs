using System.Collections.Generic;

namespace Uxios.Core.Models
{
    public class UxiosResponse<T>
    {
        public int StatusCode { get; set; }
        public Dictionary<string, string> Headers { get; set; } = new();
        public string RawBody { get; set; }
        public T Data { get; set; }
    }
}