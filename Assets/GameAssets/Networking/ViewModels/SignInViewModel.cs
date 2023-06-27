using System.ComponentModel;

namespace Networking.ViewModels
{
    public class SignInViewModel : INotifyPropertyChanged
	{
        public event PropertyChangedEventHandler PropertyChanged;

		public string Login { get; set; }
		public string Password { get; set; }
		public bool IsAuthorized { get; set; }
		public SignInModel Model { get; }

		public SignInViewModel(SignInModel signInModel, bool isAuthorized)
		{
            Model = signInModel;

            Login = signInModel.Login;
			Password = signInModel.Password;
			IsAuthorized = isAuthorized;
		}

        public void Aurhorize()
		{
            //if (true)
            //{
            //    var login = model.Login;
            //    var password = model.Password;

            //    model.Login = viewModel.Login;
            //    model.Password = viewModel.Password;
            //    var isAuthorized = viewModel.IsAuthorized;
            //    m_MessageSender.SignIn(model);
            //}
            //else
            //{

            //}
        }
	}
}