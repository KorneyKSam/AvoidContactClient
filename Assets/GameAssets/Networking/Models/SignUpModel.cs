namespace Networking
{
    public class SignUpModel
    {
        public string Login { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }

        public SignUpModel(string login, string password, string email)
        {
            Login = login;
            Password = password;
            Email = email;
        }
    }
}