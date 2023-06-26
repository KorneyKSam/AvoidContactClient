using Riptide;

namespace Networking
{
    public static class MessageReceiver
    {
        private static ISignServerCommandsExecutor m_ServerCommandsExecutor;

        public static void SetServerCommandsExecutor(ISignServerCommandsExecutor serverCommandsExecutor)
        {
            m_ServerCommandsExecutor = serverCommandsExecutor;
        }

        [MessageHandler((ushort)ServerCommands.SignInResult)]
        public static void ShowSignInResult(Message message)
        {
            m_ServerCommandsExecutor.ShowSignInResult(message.GetBool(), message.GetString());
        }

        [MessageHandler((ushort)ServerCommands.SignUpResult)]
        public static void ShowSignUpResult(Message message)
        {
            m_ServerCommandsExecutor.ShowSignUpResult(message.GetBool(), message.GetString());
        }

        [MessageHandler((ushort)ServerCommands.SignOutResult)]
        public static void ShowSignOutResult(Message message)
        {
            m_ServerCommandsExecutor.ShowSignOutResult(message.GetBool(), message.GetString());
        }
    }
}
