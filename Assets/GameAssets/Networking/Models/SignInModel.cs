using System;

namespace Networking
{
    [Serializable]
    public class SignInModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}