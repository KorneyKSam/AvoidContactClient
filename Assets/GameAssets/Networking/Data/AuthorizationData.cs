using Newtonsoft.Json;
using System;

namespace Networking.Sign.Data
{
    [Serializable]
    public class AuthorizationData
    {
        [JsonProperty("AutoAuthorization")]
        public bool IsAutomaticAuthorization { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}