using Networking;
using Networking.ViewModels;
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
