using AvoidContactCommon.Validation;
using System.Collections.Generic;

namespace Networking
{
    public static class SignValidationMessages
    {
        public const string NoConnection = "Не удалось подключиться к серверу!";

        public static Dictionary<SignInResult, string> SignInMessages = new Dictionary<SignInResult, string>()
        {
            { SignInResult.Success, "Успешная авторизация!" },
            { SignInResult.WrongLoginOrPassword, "Неверный логин или пароль!" },
            { SignInResult.AccountIsOccupied, "Аккаунт занят другим устройством!" },
        };

        public static Dictionary<SignUpResult, string> SignUpMessages = new Dictionary<SignUpResult, string>()
        {
            { SignUpResult.Success, "Успешная регистрация!" },
            { SignUpResult.LoginUsed, "Логин уже используется другим игроком!" },
            { SignUpResult.EmailUsed, "Почта уже используется!" },
            { SignUpResult.NotValidLogin, "Логин не прошёл проверку!" },
            { SignUpResult.NotValidPassword, "Пароль не прошёл проверку!" },
            { SignUpResult.NotValidEmail, "Почта не прошла проверку!" },
            { SignUpResult.NotValidLoginAndPassword, "Логин и пароль не прошли проверку!" },
            { SignUpResult.NotValidLoginAndEmail, "Логин и почта не прошли проверку!" },
            { SignUpResult.NotValidEmailAndPassword, "Почта и пароль не прошли проверку!" },
            { SignUpResult.NotValidLoginAndPasswordAndEmail, "Почта, логин и пароль не прошли проверку!" },
        };
    }
}