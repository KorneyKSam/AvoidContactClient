using UI;
using UnityEngine;
using Zenject;

public class MainMenuSceneInstaller : MonoInstaller
{
    [SerializeField]
    private MainMenuUI m_MainMenuUI;

    public override void InstallBindings()
    {
        Container.Bind<MainMenuUI>().FromInstance(m_MainMenuUI).AsSingle();
    }
}