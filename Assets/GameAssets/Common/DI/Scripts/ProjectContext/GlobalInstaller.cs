using Common;
using Common.Data;
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

    [SerializeField]
    private CameraController m_CameraController;

    public override void InstallBindings()
    {
        BindSceneLoading();
        BindNetworking();
        BindCameraController();

        Container.Bind<DataService>().FromNew().AsSingle();
    }

    private void BindSceneLoading()
    {
        var loadingScreenInstance = InstantiateWithDefaultValues(m_LoadingScreen);
        Container.Bind<UILoadingScreen>().FromInstance(loadingScreenInstance).AsCached();
        Container.QueueForInject(loadingScreenInstance);
        Container.Bind<SceneLoader>().FromNew().AsSingle();
        Container.Bind<FakeLoader>().FromNew().AsSingle();
    }

    private void BindNetworking()
    {
        Container.Bind<ISignServerCommandsExecutor>().To<SignServerCommandExecutor>().FromNew().AsSingle();
        var networkService = Container.InstantiatePrefabForComponent<NetworkService>(m_NetworkService, Vector2.zero, Quaternion.identity, null);
        Container.Bind(typeof(NetworkService), typeof(IInitializable)).FromInstance(networkService).AsSingle();
    }

    private void BindCameraController()
    {
        var cameraController = InstantiateWithDefaultValues(m_CameraController);
        Container.Bind<CameraController>().FromInstance(cameraController);
    }

    private T InstantiateWithDefaultValues<T>(T prefab) where T : MonoBehaviour
    {
        return Container.InstantiatePrefabForComponent<T>(prefab, Vector2.zero, Quaternion.identity, null);
    }
}