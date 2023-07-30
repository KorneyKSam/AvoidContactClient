using Newtonsoft.Json;
using System;

namespace Common.Data
{
    [Serializable]
    public class SignInData
    {
        [JsonProperty("AutoAuthorization")]
        public bool IsAutomaticAuthorization { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}