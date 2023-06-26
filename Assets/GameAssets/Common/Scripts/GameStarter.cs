using Networking;
using SceneLoading;
using UnityEngine;
using Zenject;

public class GameStarter : MonoBehaviour
{
    [Header("Network Connection")]
    [SerializeField]
    private string m_Ip = "127.0.0.1";

    [SerializeField]
    private ushort m_Port = 7777;

    [Inject]
    private SceneLoader m_SceneLoader;

    [Inject]
    private NetworkService m_NetworkSerivce;

    private void Start()
    {
        if (!m_NetworkSerivce.IsConnected)
        {
            m_NetworkSerivce.Connect(m_Ip, m_Port);
        }
        //NetworkSerivce.Instance.Connect();
    }
}
