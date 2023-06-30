using MVVM;
using Networking;
using SceneLoading;
using UI;
using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [Header("Network Connection")]
    [SerializeField]
    private string m_Ip = "127.0.0.1";

    [SerializeField]
    private ushort m_Port = 7777;

    [SerializeField]
    private SignInView m_SignInView;

    private SignInViewModel m_SignInViewModel;

    [Header("Fake loading")]
    private int m_LoadingMilliseconds = 1000;

    [Inject]
    private FakeLoader m_FakeLoader;

    [Inject]
    private UILoadingScreen m_LoadingScreen;

    [Inject]
    private NetworkService m_NetworkSerivce;

    [Inject]
    private MainMenuUI m_MainMenuUI;

    private void Start()
    {
        StartConnecting();
        m_SignInViewModel = new SignInViewModel();
        m_SignInView.SetVeiwModel(m_SignInViewModel);
    }

    [ContextMenu("TEST1")]
    private void TEST1()
    {
        m_SignInViewModel.Login = "123";
    }


    [ContextMenu("TEST2")]
    private void TEST2()
    {
        m_SignInViewModel.Login = "321";
    }


    private void StartConnecting()
    {
        if (!m_NetworkSerivce.IsConnected)
        {
            m_FakeLoader.ShowFakeLoadAnimation(m_LoadingMilliseconds, allowActivation: false, useResumeButton: false);
            m_NetworkSerivce.Connect(m_Ip, m_Port);
            m_NetworkSerivce.OnConnectionResult += OnConnectionResult;
        }
    }

    private void OnConnectionResult(bool isConnected)
    {
        m_FakeLoader.AllowActivation(true);
        m_MainMenuUI.NavigationPanel.MoveToMiddle();
        m_MainMenuUI.NavigationPanel.SetNavigationContent(NavigationPanelContent.Authorization);



        //if (isConnected)
        //{
        //}
    }
}
