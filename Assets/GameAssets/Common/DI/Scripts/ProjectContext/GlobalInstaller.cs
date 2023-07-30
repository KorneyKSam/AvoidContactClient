using Common;
using Networking;
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

    [SerializeField]
    private CameraController m_CameraController;

    public override void InstallBindings()
    {
        BindData();
        BindSceneLoading();
        BindNetworking();
        BindCameraController();
    }

    private void BindData()
    {
        Container.Bind<DataService>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<SignerInfo>().FromNew().AsSingle();
        Container.BindInterfacesAndSelfTo<ServerConnectionInfo>().FromNew().AsSingle();
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
        Container.Bind<MessageSender>().FromNew().AsSingle();
        var networkService = Container.InstantiatePrefabForComponent<ServerConnector>(m_ServerConnector, Vector2.zero, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<ServerConnector>().FromInstance(networkService).AsSingle();
        Container.BindInterfacesAndSelfTo<ServerSigner>().FromNew().AsSingle();
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