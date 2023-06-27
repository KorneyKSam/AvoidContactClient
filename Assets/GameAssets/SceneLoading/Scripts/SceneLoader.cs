using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
    public class SceneLoader : BaseLoader
	{
		private AsyncOperation m_SceneLoadingOperation;

        public void LoadScene(int buildIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool allowSceneActivation = true, bool useResumeButton = true)
        {
            var sceneName = SceneManager.GetSceneByBuildIndex(buildIndex).name;
            LoadScene(sceneName, loadSceneMode, allowSceneActivation);
		}

        public void LoadScene(SceneName sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool allowSceneActivation = true, bool useResumeButton = true)
        {
            LoadScene(sceneName.ToString(), loadSceneMode, allowSceneActivation);
        }

        public void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool allowSceneActivation = true, bool useResumeButton = true)
		{
            LoadSceneAsync(sceneName, loadSceneMode, allowSceneActivation, useResumeButton);
		}

		public void AllowSceneActivation(bool isAllowed)
		{
			if (m_SceneLoadingOperation != null)
			{
				IsActivationAllowed = isAllowed;
            }
		}

        protected override void FinishLoading()
        {
            if (m_SceneLoadingOperation != null)
            {
                m_SceneLoadingOperation.allowSceneActivation = true;
                LoadingScreen.SetActive(false);
            }
        }

        private async void LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode, bool allowSceneActivation, bool useResumeButton)
		{
            IsActivationAllowed = allowSceneActivation;
			ActivateLoadingScreen(useResumeButton);

            m_SceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            m_SceneLoadingOperation.allowSceneActivation = false;

			while (m_SceneLoadingOperation.progress < 0.9f)
			{
                LoadingScreen.SetLoadingProgress(m_SceneLoadingOperation.progress);
				await Task.Delay(LoadingDelay);
            }

            LoadingScreen.SetLoadingProgress(LoadingScreen.FullProgress);
        }
	}
}