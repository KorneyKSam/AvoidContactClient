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
        private bool m_IsResumeButtonUsing;

        protected void ActivateLoadingScreen(bool useResumeButton)
        {
            m_IsResumeButtonUsing = useResumeButton;
            LoadingScreen.SetActive(true);
            LoadingScreen.ResetProgress();
            AddLoadingScreenListeners();
        }

        protected abstract void FinishLoading();

        private void AddLoadingScreenListeners()
        {
            RemoveLoadingScreenListeners();
            LoadingScreen.OnProgressComplete += SetLoadingScreenFinished;
            if (m_IsResumeButtonUsing)
            {
                LoadingScreen.OnButtonPressed += OnButtonPressed;
            }
        }

        private void RemoveLoadingScreenListeners()
        {
            LoadingScreen.OnProgressComplete -= SetLoadingScreenFinished;
            LoadingScreen.OnButtonPressed -= OnButtonPressed;
        }

        private void AllowResumeButton(bool isAllowed)
        {
            LoadingScreen.AllowToClickResumeButton(isAllowed && m_IsResumeButtonUsing);

            if (isAllowed && !m_IsResumeButtonUsing)
            {
                OnButtonPressed();
            }
        }

        private void OnButtonPressed()
        {
            LoadingScreen.OnButtonPressed -= OnButtonPressed;
            LoadingScreen.ResetProgress();
            AllowResumeButton(false);
            FinishLoading();
        }

        private void SetLoadingScreenFinished()
        {
            LoadingScreen.OnProgressComplete -= SetLoadingScreenFinished;
            AllowResumeButton(IsActivationAllowed);
        }
    }
}