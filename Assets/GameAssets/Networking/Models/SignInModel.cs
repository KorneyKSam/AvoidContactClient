using Common.Data;
using System;

namespace Networking
{
    [Serializable]
    public class SignInModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public SignInModel()
        {
            Login = string.Empty;
            Password = string.Empty;
        }
    }
}