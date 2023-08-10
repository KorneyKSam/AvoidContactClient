namespace Networking.Sign
{
    public enum SignInResult : byte
    {
        Success = 0,
        WrongLoginOrPassword = 1,
        AccountIsOccupied = 2,
    }
}