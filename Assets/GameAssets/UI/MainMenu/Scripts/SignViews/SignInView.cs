using MVVM;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class SignInView : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField m_LoginField;

        [SerializeField]
        private TMP_InputField m_PasswordField;

        [SerializeField]
        private Toggle m_SignAuthomatically;

        [SerializeField]
        private Button m_SignInButton;

        [SerializeField]
        private Button m_SignUpButton;

		private SignInViewModel m_Model;

        public void SetVeiwModel(SignInViewModel model)
		{
			m_Model = model;
			var binder = new PropertyBinder<string>(m_Model, 
													nameof(m_Model.Login), 
													(value) => m_LoginField.text = value);
			
			binder.CreateReverseBinding(m_LoginField.onValueChanged, 
										(value) => m_Model.Login = value);
		}
	} 
}
