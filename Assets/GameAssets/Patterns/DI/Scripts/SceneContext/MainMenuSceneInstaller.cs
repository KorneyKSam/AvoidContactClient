using Common;
using DialogBoxService;
using UI;
using UnityEngine;
using Zenject;

namespace DI
{
    public class MainMenuSceneInstaller : MonoInstaller
    {
        [SerializeField]
        private MainMenuController m_MainMenuController;

        [SerializeField]
        private DialogService m_DialogService;

        [SerializeField]
        private CameraController m_CameraController;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainMenuController>().FromInstance(m_MainMenuController).AsSingle();
            Container.Bind<DialogService>().FromInstance(m_DialogService).AsSingle();
            Container.Bind<CameraController>().FromInstance(m_CameraController).AsSingle();
        }
    }
}