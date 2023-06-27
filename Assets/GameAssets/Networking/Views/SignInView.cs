using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Networking.ViewModels
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
        private SignInViewModel m_DataContext;

        public SignInViewModel DataContext
        {
            get => m_DataContext; 
            set
            {
                if (m_DataContext == value)
                {
                    return;
                }

                m_DataContext = value;
                m_LoginField.text = value.Login;
                m_PasswordField.text = value.Password;
                m_SignInButton.onClick.AddListener(m_DataContext.Aurhorize);
            }
        }
    }
}
