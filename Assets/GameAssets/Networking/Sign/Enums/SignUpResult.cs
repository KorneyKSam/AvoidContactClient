namespace Networking.Enums
{
    public enum SignUpResult : byte
    {
        Success = 0,
        LoginUsed = 1,
        EmailUsed = 2,
        NotValidLogin = 3,
        NotValidPassword = 4,
        NotValidEmail = 5,
        NotValidLoginAndPassword = 6,
        NotValidLoginAndEmail = 7,
        NotValidEmailAndPassword = 8,
        NotValidLoginAndPasswordAndEmail = 9,
    }
}