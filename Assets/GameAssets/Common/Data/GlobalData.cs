using Newtonsoft.Json;
using System;

namespace Common.Data
{
    [Serializable]
    public class GlobalData
    {
        [JsonProperty("AutoAuthorization")]
        public bool IsAutomaticAuthorization { get; set; }
    }
}