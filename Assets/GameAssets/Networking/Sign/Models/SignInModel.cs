using System;

namespace Networking
{
    [Serializable]
    public class SignInModel
    {
        public string Login { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}