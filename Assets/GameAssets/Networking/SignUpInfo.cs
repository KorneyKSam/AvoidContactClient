using System;

namespace Networking.Sign
{
    [Serializable]
    public class SignUpInfo
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}