using AvoidContactCommon.Validation;
using System.Collections.Generic;

namespace Networking
{
    public static class SignValidationMessages
    {
        public const string NoConnection = "Не удалось подключиться к серверу!";
        public const string Info = "Нажмите на ⓘ чтобы узнать подробности!";

        public static Dictionary<SignInResult, string> SignInMessages = new()
        {
            { SignInResult.Success, "Успешная авторизация!" },
            { SignInResult.WrongLoginOrPassword, "Неверный логин или пароль!" },
            { SignInResult.AccountIsOccupied, "Аккаунт занят другим устройством!" },
            { SignInResult.NotValidLoginOrPassword, $"Невалидный логин или пароль! {Info}" },
        };

        public static Dictionary<SignUpResult, string> SignUpMessages = new()
        {
            { SignUpResult.Success, "Успешная регистрация!" },
            { SignUpResult.LoginUsed, "Логин уже используется другим игроком!" },
            { SignUpResult.EmailUsed, "Почта уже используется!" },
            { SignUpResult.NotValidLogin, $"Логин не прошёл проверку! {Info}" },
            { SignUpResult.NotValidPassword, $"Пароль не прошёл проверку! {Info}" },
            { SignUpResult.NotValidEmail, $"Почта не прошла проверку! {Info}" },
            { SignUpResult.NotValidLoginAndPassword, $"Логин и пароль не прошли проверку! {Info}" },
            { SignUpResult.NotValidLoginAndEmail, $"Логин и почта не прошли проверку! {Info}" },
            { SignUpResult.NotValidEmailAndPassword, $"Почта и пароль не прошли проверку! {Info}" },
            { SignUpResult.NotValidLoginAndPasswordAndEmail, $"Почта, логин и пароль не прошли проверку! {Info}" },
            { SignUpResult.NotValidCallSign, $"Позывной не прошёл проверку! {Info}" },
            { SignUpResult.NotValidDescription, "Описание превышает лимит в 495 символов, интерфейс не резиновый! Вы решили свой роман «Война и Мир» написать?" },
        };
    }
}