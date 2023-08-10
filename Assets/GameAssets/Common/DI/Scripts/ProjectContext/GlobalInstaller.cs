using Common;
using Networking;
using Networking.Sign;
using Riptide;
using SceneLoading;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField]
    private UILoadingScreen m_LoadingScreen;

    [SerializeField]
    private ServerConnector m_ServerConnector;

    public override void InstallBindings()
    {
        BindData();
        BindSceneLoading();
        BindNetworking();
    }

    private void BindData()
    {
        Container.Bind<DataService>().FromNew().AsSingle();
    }

    private void BindSceneLoading()
    {
        var loadingScreenInstance = InstantiateWithDefaultValues(m_LoadingScreen);
        Container.Bind<UILoadingScreen>().FromInstance(loadingScreenInstance).AsSingle();
        Container.QueueForInject(loadingScreenInstance);
        Container.Bind<SceneLoader>().FromNew().AsSingle();
        Container.Bind<FakeLoader>().FromNew().AsSingle();
    }

    private void BindNetworking()
    {
        Container.Bind<Client>().FromNew().AsSingle();
        Container.Bind<SignMessageSender>().FromNew().AsSingle();
        var networkService = Container.InstantiatePrefabForComponent<ServerConnector>(m_ServerConnector, Vector2.zero, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<ServerConnector>().FromInstance(networkService).AsSingle();
        Container.BindInterfacesAndSelfTo<SignService>().FromNew().AsSingle();
    }

    private T InstantiateWithDefaultValues<T>(T prefab) where T : MonoBehaviour
    {
        return Container.InstantiatePrefabForComponent<T>(prefab, Vector2.zero, Quaternion.identity, null);
    }
}