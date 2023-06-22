using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneLoading
{
    public class LoadingScreen : MonoBehaviour
    {
        public event Action OnProgressComplete;

        private const float FullProgress = 1f;
        private const float LerpDuration = 1.5f;
        private const float FadeDuration = 0.5f;

        [SerializeField]
        private CanvasGroup m_LoadingScreenCanvas;

        [SerializeField]
        private Image m_ProgressBar;

        [SerializeField]
        private TMP_Text m_PressButtonToContinueText;

        private Coroutine m_Coroutine;
        private float m_TargetProgress;
        private float m_TimeElapsed;

        public void SetActive(bool isActive)
        {
            if (isActive)
            {
                m_LoadingScreenCanvas.DOFade(0f, 0f);
                m_LoadingScreenCanvas.DOFade(1f, FadeDuration);
            }
            else
            {
                m_LoadingScreenCanvas.DOFade(0f, FadeDuration);
            }

        }

        public void ResetProgress()
        {
            m_ProgressBar.fillAmount = 0f;
            if (m_Coroutine != null)
            {
                StopCoroutine(m_Coroutine);
                m_Coroutine = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress">Set parameter from 0f to 1f</param>
        public void SetLoadingProgress(float progress)
        {
            m_TargetProgress = progress >= FullProgress ? FullProgress : progress;

            if (m_Coroutine == null)
            {
                m_Coroutine = StartCoroutine(SetProgressSmoothly());
            }
        }

        private IEnumerator SetProgressSmoothly()
        {
            m_TimeElapsed = 0;
            var waitForEndOfFrame = new WaitForEndOfFrame();

            while (m_TimeElapsed < LerpDuration)
            {
                m_ProgressBar.fillAmount = Mathf.Lerp(m_ProgressBar.fillAmount, m_TargetProgress, m_TimeElapsed / LerpDuration);
                m_TimeElapsed += Time.deltaTime;
                yield return waitForEndOfFrame;
            }

            m_ProgressBar.fillAmount = m_TargetProgress;

            if (m_ProgressBar.fillAmount == FullProgress)
            {
                m_PressButtonToContinueText.gameObject.SetActive(true);
                StartCoroutine(TrackEnterPressed());
            }

            m_Coroutine = null;
        }

        private IEnumerator TrackEnterPressed()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SetProgressComplete();
                    yield break;
                }

                yield return null;
            }
        }

        private void SetProgressComplete()
        {
            m_PressButtonToContinueText.gameObject.SetActive(false);
            OnProgressComplete?.Invoke();
        }
    }
}
