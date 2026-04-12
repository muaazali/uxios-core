using System;

namespace Uxios.Core.Models
{
    public class UxiosFile
    {
        public string FieldName { get; set; } // e.g. "file" or "image"
        public string FileName { get; set; } // e.g. "photo.png"
        public byte[] Data { get; set; }
        public string ContentType { get; set; } = "application/octet-stream";
    }
}