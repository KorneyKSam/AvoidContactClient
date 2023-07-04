using UnityEngine.Events;

namespace Networking
{
    public static class SignEventsHolder
    {
        public static UnityEvent<bool, string> OnSignIn = new();
        public static UnityEvent<bool, string> OnSignUp = new();
        public static UnityEvent<bool, string> OnSignOut = new();

        public static void InvokeSignIn(bool success, string message)
        {
            OnSignIn.Invoke(success, message);
        }

        public static void InvokeSignOut(bool success, string message)
        {
            OnSignOut.Invoke(success, message);
        }

        public static void InvokeSignUp(bool success, string message)
        {
            OnSignUp.Invoke(success, message);
        }
    }
}
