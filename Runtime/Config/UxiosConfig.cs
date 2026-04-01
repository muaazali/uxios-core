using System;
using System.Collections.Generic;
using Uxios.Core.Plugins;
using Uxios.Core.Serialization;

namespace Uxios.Core.Config
{
    public class UxiosConfig
    {
        public string BaseUrl { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 30;
        public Dictionary<string, string> DefaultHeaders { get; set; } = new();
        public IUxiosSerializer Serializer { get; set; } = new JsonUtilitySerializer();
        public List<IUxiosPlugin> Plugins { get; set; } = new();
    }
}