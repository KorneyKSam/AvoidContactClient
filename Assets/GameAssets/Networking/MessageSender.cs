using Riptide;

namespace Networking
{
    public class MessageSender
    {
        private Client m_Client;

        public MessageSender(Client client)
        {
            m_Client = client;
        }

        public void SignIn(SignInModel signInModel)
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientCommands.SignIn);
            message.AddString(signInModel.Login);
            message.AddString(signInModel.Password);
            m_Client.Send(message);
        }

        public void SignUp(SignUpModel signUpModel)
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientCommands.SignUp);
            message.AddString(signUpModel.Login);
            message.AddString(signUpModel.Password);
            message.AddString(signUpModel.Email);
            m_Client.Send(message);
        }

        public void SignOut()
        {
            var message = Message.Create(MessageSendMode.Reliable, ClientCommands.SignOut);
            m_Client.Send(message);
        }
    }
}
