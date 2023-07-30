using Networking.Enums;
using UnityEngine.Events;

namespace Networking
{
    public static class SignEventsHolder
    {
        public static UnityEvent<SignInResult> OnSignIn = new();
        public static UnityEvent<SignUpResult> OnSignUp = new();
        public static UnityEvent<bool, string> OnSignOut = new();

        public static void InvokeSignIn(SignInResult signInResult)
        {
            OnSignIn.Invoke(signInResult);
        }

        public static void InvokeSignOut(bool success, string message)
        {
            OnSignOut.Invoke(success, message);
        }

        public static void InvokeSignUp(SignUpResult signUpResult)
        {
            OnSignUp.Invoke(signUpResult);
        }
    }
}
