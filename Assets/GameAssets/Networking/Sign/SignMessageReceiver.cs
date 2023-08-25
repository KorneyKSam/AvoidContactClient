using AvoidContactCommon.Sign.Messages;
using AvoidContactCommon.Validation;
using Riptide;
using UnityEngine.Events;

namespace Networking.Sign
{
    public static class SignMessageReceiver
    {
        public static UnityEvent<SignInResult, string> OnSignInResultRecieve = new();
        public static UnityEvent<SignUpResult> OnSignUpResultReceive = new();
        public static UnityEvent<bool> OnCommonInfoChange = new();
        public static UnityEvent OnSignOutResultReceive = new();

        [MessageHandler((ushort)ServerSignMessage.SignInResult)]
        public static void ShowSignInResult(Message message)
        {
            OnSignInResultRecieve?.Invoke((SignInResult)message.GetByte(), message.GetString());
        }

        [MessageHandler((ushort)ServerSignMessage.SignUpResult)]
        public static void ShowSignUpResult(Message message)
        {
            OnSignUpResultReceive?.Invoke((SignUpResult)message.GetByte());
        }
    }
}
