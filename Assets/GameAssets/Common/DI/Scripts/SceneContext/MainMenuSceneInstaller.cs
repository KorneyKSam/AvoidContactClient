using UI;
using UnityEngine;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField]
    private MainMenuController m_MainMenuController;

    [SerializeField]
    private NavigationPanelConroller m_NavigationPanelConroller;

    public override void InstallBindings()
    {
        Container.Bind<NavigationPanelConroller>().FromInstance(m_NavigationPanelConroller).AsSingle();
        Container.Bind<MainMenuController>().FromInstance(m_MainMenuController).AsSingle();
    }
}