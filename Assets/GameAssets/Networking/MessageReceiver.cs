using Networking.Enums;
using Riptide;

namespace Networking
{
    public static class MessageReceiver
    {
        [MessageHandler((ushort)ServerCommands.SignInResult)]
        public static void ShowSignInResult(Message message)
        {
            SignEventsHolder.InvokeSignIn((SignInResult)message.GetByte());
        }

        [MessageHandler((ushort)ServerCommands.SignUpResult)]
        public static void ShowSignUpResult(Message message)
        {
            SignEventsHolder.InvokeSignUp((SignUpResult)message.GetByte());
        }

        [MessageHandler((ushort)ServerCommands.SignOutResult)]
        public static void ShowSignOutResult(Message message)
        {
            SignEventsHolder.InvokeSignOut(message.GetBool(), message.GetString());
        }
    }
}
