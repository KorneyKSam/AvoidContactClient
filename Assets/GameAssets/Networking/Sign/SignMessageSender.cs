using AvoidContactCommon.Sign;
using AvoidContactCommon.Sign.Messages;
using Riptide;
using Zenject;

namespace Networking.Sign
{
    public class SignMessageSender
    {
        [Inject]
        private Client m_Client;

        public void SignIn(string login, string password)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientSignMessage.SignIn);
            message.AddString(login);
            message.AddString(password);
            m_Client.Send(message);
        }

        public void SignUp(SignInfo signedPlayerInfo)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientSignMessage.SignUp);
            message.AddString(signedPlayerInfo.Login);
            message.AddString(signedPlayerInfo.Password);
            message.AddString(signedPlayerInfo.Email);
            m_Client.Send(message);
        }

        public void SignOut()
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientSignMessage.SignOut);
            m_Client.Send(message);
        }

        public void UpdateCommonInfo(PlayerInfo commonPlayerInfo)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientSignMessage.UpdateCommonInfo);
            message.AddString(commonPlayerInfo.CallSign);
            message.AddString(commonPlayerInfo.PlayerDiscription);
            m_Client.Send(message);
        }

        public void LinkToken(string authorizationToken)
        {
            var message = Message.Create(MessageSendMode.Reliable, (ushort)ClientSignMessage.LinkToken);
            message.AddString(authorizationToken);
            m_Client.Send(message);
        }
    }
}
