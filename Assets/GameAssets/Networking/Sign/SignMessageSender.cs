using Riptide;
using UnityEngine;
using Zenject;

namespace Networking.Sign
{
    public class SignMessageSender
    {
        [Inject]
        private Client m_Client;

        public void SignIn(string login, string password)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientCommands.SignIn);
            message.AddString(login);
            message.AddString(password);
            m_Client.Send(message);
        }

        public void SignUp(SignUpInfo signUpModel)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientCommands.SignUp);
            message.AddString(signUpModel.Login);
            message.AddString(signUpModel.Password);
            message.AddString(signUpModel.Email);
            m_Client.Send(message);
        }

        public void SignOut()
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientCommands.SignOut);
            m_Client.Send(message);
        }

        public void LinkToken(string authorizationToken)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientCommands.LinkToken);
            message.AddString(authorizationToken);
            m_Client.Send(message);
        }
    }
}
