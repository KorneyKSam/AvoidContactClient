using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneLoading
{
	public class SceneLoader : Singleton<SceneLoader>
	{
		private const int LoadingDelay = 10;
		private const float FullProgress = 1f;

		[SerializeField]
		private LoadingScreen m_LoadingScreen;

		private AsyncOperation m_SceneLoadingOperation;
		private bool m_IsLoadingScreenFinished;
		private bool m_AllowSceneActivation;

        public void LoadScene(int buildIndex, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool allowSceneActivation = true)
        {
            var sceneName = SceneManager.GetSceneByBuildIndex(buildIndex).name;
            LoadScene(sceneName);
		}

		public void LoadScene(string sceneName, LoadSceneMode loadSceneMode = LoadSceneMode.Single, bool allowSceneActivation = true)
		{
            LoadSceneAsync(sceneName, loadSceneMode, allowSceneActivation);
		}

		public void AllowSceneActivation(bool isAllowed)
		{
			if (m_SceneLoadingOperation != null)
			{
				m_AllowSceneActivation = isAllowed;
                TryToSetAllowActivation();
            }
		}

		private async void LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode, bool allowSceneActivation)
		{
			m_IsLoadingScreenFinished = false;
            m_AllowSceneActivation = allowSceneActivation;

			m_LoadingScreen.SetActive(true);
            m_LoadingScreen.ResetProgress();
            m_LoadingScreen.OnProgressComplete += SetLoadingScreenFinished;

            m_SceneLoadingOperation = SceneManager.LoadSceneAsync(sceneName, loadSceneMode);
            m_SceneLoadingOperation.allowSceneActivation = false;

			while (m_SceneLoadingOperation.progress < 0.9f)
			{
				m_LoadingScreen.SetLoadingProgress(m_SceneLoadingOperation.progress);
				await Task.Delay(LoadingDelay);
            }

			m_LoadingScreen.SetLoadingProgress(FullProgress);
        }

		private void SetLoadingScreenFinished()
		{
			m_IsLoadingScreenFinished = true;
			m_LoadingScreen.OnProgressComplete -= SetLoadingScreenFinished;
			TryToSetAllowActivation();
		}

		private void TryToSetAllowActivation()
		{
			if (m_SceneLoadingOperation != null)
			{
				bool allowActivation = m_IsLoadingScreenFinished && m_AllowSceneActivation;
                m_SceneLoadingOperation.allowSceneActivation = allowActivation;
                m_LoadingScreen.SetActive(!allowActivation);
			}
		}
	}
}