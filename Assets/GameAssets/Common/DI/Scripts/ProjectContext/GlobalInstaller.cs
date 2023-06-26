using Networking;
using SceneLoading;
using UnityEngine;
using Zenject;

public class GlobalInstaller : MonoInstaller
{
    [SerializeField]
    private UILoadingScreen m_LoadingScreen;

    [SerializeField]
    private NetworkService m_NetworkService;

    public override void InstallBindings()
    {
        BindSceneLoading();

        BindNetworking();
    }

    private void BindSceneLoading()
    {
        var loadingScreenInstance = Container.InstantiatePrefabForComponent<UILoadingScreen>(m_LoadingScreen, Vector2.zero, Quaternion.identity, null);
        Container.Bind<UILoadingScreen>().FromInstance(loadingScreenInstance).AsSingle();
        loadingScreenInstance.SetActive(false, useFadeDuration: false);
        Container.QueueForInject(loadingScreenInstance);
        Container.Bind<SceneLoader>().FromNew().AsSingle();
        Container.Bind<FakeLoader>().FromNew().AsSingle();
    }

    private void BindNetworking()
    {
        Container.Bind<ISignServerCommandsExecutor>().To<SignServerCommandExecutor>().FromNew().AsSingle();
        var networkService = Container.InstantiatePrefabForComponent<NetworkService>(m_NetworkService, Vector2.zero, Quaternion.identity, null);
        Container.Bind<NetworkService>().FromInstance(networkService).AsSingle();
    }
}