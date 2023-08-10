using System;

namespace Networking.Sign
{
    [Serializable]
    public class SignUpModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}