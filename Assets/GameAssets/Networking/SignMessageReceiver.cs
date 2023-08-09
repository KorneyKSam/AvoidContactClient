using Riptide;
using UnityEngine.Events;

namespace Networking
{
    public static class SignMessageReceiver
    {
        public static UnityEvent<bool> OnSignInResultRecieve = new();
        public static UnityEvent<bool, string> OnSignUpResultReceive = new();
        public static UnityEvent OnSignOutResultReceive = new();

        [MessageHandler((ushort)ServerCommands.SignInResult)]
        public static void ShowSignInResult(Message message)
        {
            OnSignInResultRecieve?.Invoke(message.GetBool());
        }

        [MessageHandler((ushort)ServerCommands.SignUpResult)]
        public static void ShowSignUpResult(Message message)
        {
            OnSignUpResultReceive?.Invoke(message.GetBool(), message.GetString());
        }

        [MessageHandler((ushort)ServerCommands.SignOutResult)]
        public static void ShowSignOutResult(Message message)
        {
            OnSignOutResultReceive?.Invoke();
        }
    }
}
