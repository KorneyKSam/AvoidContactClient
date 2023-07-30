using Riptide;
using Zenject;

namespace Networking
{
    public class MessageSender
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

        public void SignUp(SignUpModel signUpModel)
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
    }
}
