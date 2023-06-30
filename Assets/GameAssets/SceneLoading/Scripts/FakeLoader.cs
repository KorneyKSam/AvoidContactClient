using System;
using System.Threading.Tasks;
using UnityEngine;

namespace SceneLoading
{
    public class FakeLoader : BaseLoader
    {
        private const int FullPercentage = 100;

        private int m_Milliseconds;
        private int m_CurrentMilliseconds;

        public void ShowFakeLoadAnimation(int milliseconds, bool allowActivation = true, bool useResumeButton = true)
        {
            m_CurrentMilliseconds = 0;
            m_Milliseconds = milliseconds;
            IsActivationAllowed = allowActivation;
            LoadAsync(useResumeButton);
        }

        public void AllowActivation(bool isAllowed)
        {
            IsActivationAllowed = isAllowed;
        }

        protected override void FinishLoading()
        {
            LoadingScreen.SetActive(false);
        }

        private async void LoadAsync(bool useResumeButton)
        {
            ActivateLoadingScreen(useResumeButton);

            while (m_CurrentMilliseconds <= m_Milliseconds)
            {
                float percentComplete = (float)Math.Round((double)(FullPercentage * m_CurrentMilliseconds) / m_Milliseconds);
                float value = (percentComplete / FullPercentage) * LoadingScreen.FullProgress;
                LoadingScreen.SetLoadingProgress(value);
                await Task.Delay(LoadingDelay);
                m_CurrentMilliseconds += LoadingDelay;
            }
        }
    }
}