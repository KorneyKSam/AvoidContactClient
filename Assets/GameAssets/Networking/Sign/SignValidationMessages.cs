using AvoidContactCommon.Validation;
using System.Collections.Generic;

namespace Networking
{
    public static class SignValidationMessages
    {
        public const string NoConnection = "�� ������� ������������ � �������!";

        public static Dictionary<SignInResult, string> SignInMessages = new Dictionary<SignInResult, string>()
        {
            { SignInResult.Success, "�������� �����������!" },
            { SignInResult.WrongLoginOrPassword, "�������� ����� ��� ������!" },
            { SignInResult.AccountIsOccupied, "������� ����� ������ �����������!" },
        };

        public static Dictionary<SignUpResult, string> SignUpMessages = new Dictionary<SignUpResult, string>()
        {
            { SignUpResult.Success, "�������� �����������!" },
            { SignUpResult.LoginUsed, "����� ��� ������������ ������ �������!" },
            { SignUpResult.EmailUsed, "����� ��� ������������!" },
            { SignUpResult.NotValidLogin, "����� �� ������ ��������!" },
            { SignUpResult.NotValidPassword, "������ �� ������ ��������!" },
            { SignUpResult.NotValidEmail, "����� �� ������ ��������!" },
            { SignUpResult.NotValidLoginAndPassword, "����� � ������ �� ������ ��������!" },
            { SignUpResult.NotValidLoginAndEmail, "����� � ����� �� ������ ��������!" },
            { SignUpResult.NotValidEmailAndPassword, "����� � ������ �� ������ ��������!" },
            { SignUpResult.NotValidLoginAndPasswordAndEmail, "�����, ����� � ������ �� ������ ��������!" },
        };
    }
}