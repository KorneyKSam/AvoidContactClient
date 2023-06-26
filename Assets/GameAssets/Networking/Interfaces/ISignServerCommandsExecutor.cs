namespace Networking
{
    public interface ISignServerCommandsExecutor
    {
        public void ShowSignInResult(bool success, string message);
        public void ShowSignUpResult(bool success, string message);
        public void ShowSignOutResult(bool success, string message);
    }
}