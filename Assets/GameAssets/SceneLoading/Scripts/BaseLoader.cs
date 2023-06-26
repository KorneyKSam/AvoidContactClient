using Zenject;

namespace SceneLoading
{
    public abstract class BaseLoader
    {
        protected const int LoadingDelay = 10;

        [Inject]
        protected UILoadingScreen LoadingScreen;

        protected bool IsActivationAllowed
        {
            get => m_IsActivationAllowed;
            set
            {
                m_IsActivationAllowed = value;
                AllowResumeButton(m_IsActivationAllowed && LoadingScreen.IsProgressComplete);
            }
        }

        private bool m_IsActivationAllowed;

        protected void ActivateLoadingScreen()
        {
            LoadingScreen.SetActive(true);
            LoadingScreen.ResetProgress();
            AddLoadingScreenListeners();
        }

        protected abstract void AllowActivation();

        private void AddLoadingScreenListeners()
        {
            RemoveLoadingScreenListeners();
            LoadingScreen.OnProgressComplete += SetLoadingScreenFinished;
            LoadingScreen.OnButtonPressed += OnButtonPressed;
        }

        private void RemoveLoadingScreenListeners()
        {
            LoadingScreen.OnProgressComplete -= SetLoadingScreenFinished;
            LoadingScreen.OnButtonPressed -= OnButtonPressed;
        }

        private void AllowResumeButton(bool isAllowed)
        {
            LoadingScreen.AllowToClickResumeButton(isAllowed);
        }

        private void OnButtonPressed()
        {
            LoadingScreen.OnButtonPressed -= OnButtonPressed;
            LoadingScreen.ResetProgress();
            AllowResumeButton(false);
            AllowActivation();
        }

        private void SetLoadingScreenFinished()
        {
            LoadingScreen.OnProgressComplete -= SetLoadingScreenFinished;
            AllowResumeButton(IsActivationAllowed);
        }
    }
}