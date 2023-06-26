namespace Networking
{
    public class SignInModel
    {
        public string Login { get; set; }
        public string Password { get; set; }

        public SignInModel(string login, string password)
        {
            Login = login;
            Password = password;
        }
    }
}