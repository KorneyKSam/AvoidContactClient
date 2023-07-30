using UI;
using UI.Popups;
using UnityEngine;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField]
    private MainMenuController m_MainMenuController;

    [SerializeField]
    private NavigationPanelConroller m_NavigationPanelConroller;

    [SerializeField]
    private PopupController m_PopupController;

    public override void InstallBindings()
    {
        Container.Bind<NavigationPanelConroller>().FromInstance(m_NavigationPanelConroller).AsSingle();
        Container.BindInterfacesAndSelfTo<MainMenuController>().FromInstance(m_MainMenuController).AsSingle();
        Container.Bind<PopupController>().FromInstance(m_PopupController).AsSingle();
    }
}