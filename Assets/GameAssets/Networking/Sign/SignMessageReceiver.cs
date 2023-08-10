using Riptide;
using UnityEngine;
using UnityEngine.Events;

namespace Networking.Sign
{
    public static class SignMessageReceiver
    {
        public static UnityEvent<SignInResult, string> OnSignInResultRecieve = new();
        public static UnityEvent<SignUpResult> OnSignUpResultReceive = new();
        public static UnityEvent OnSignOutResultReceive = new();

        [MessageHandler((ushort)ServerCommands.SignInResult)]
        public static void ShowSignInResult(Message message)
        {
            OnSignInResultRecieve?.Invoke((SignInResult)message.GetByte(), message.GetString());
        }

        [MessageHandler((ushort)ServerCommands.SignUpResult)]
        public static void ShowSignUpResult(Message message)
        {
            OnSignUpResultReceive?.Invoke((SignUpResult)message.GetByte());
        }
    }
}
