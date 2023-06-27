using DG.Tweening;
using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace SceneLoading
{
    public class UILoadingScreen : MonoBehaviour
    {
        public event Action OnProgressComplete;
        public event Action OnButtonPressed;

        public bool IsProgressComplete => m_ProgressBar.fillAmount == m_FullProgress;
        public float FullProgress => m_FullProgress;

        private const float LerpDuration = 1.5f;
        private const float FadeDuration = 0.5f;

        [SerializeField]
        private CanvasGroup m_LoadingScreenCanvas;

        [SerializeField]
        private Image m_ProgressBar;

        [SerializeField]
        private TMP_Text m_PressButtonToContinueText;

        private Coroutine m_SmoothProgressCoroutine;
        private Coroutine m_ResumeCoroutine;
        private float m_FullProgress = 1f;
        private float m_TargetProgress;
        private float m_TimeElapsed;

        public void SetActive(bool isActive, bool useFadeDuration = true)
        {
            if (isActive)
            {
                m_LoadingScreenCanvas.DOFade(0f, 0f);
                SetScreenCanvasActive(true);
                m_LoadingScreenCanvas.DOFade(1f, useFadeDuration ? FadeDuration : 0f);
            }
            else
            {
                m_LoadingScreenCanvas.DOFade(0f, useFadeDuration ? FadeDuration : 0f)
                                     .OnComplete(() => SetScreenCanvasActive(false));
            }
        }

        public void ResetProgress()
        {
            m_ProgressBar.fillAmount = 0f;
            if (m_SmoothProgressCoroutine != null)
            {
                StopCoroutine(m_SmoothProgressCoroutine);
                m_SmoothProgressCoroutine = null;
            }
        }

        public void AllowToClickResumeButton(bool isAllowed)
        {
            m_PressButtonToContinueText.gameObject.SetActive(isAllowed);
            if (isAllowed && m_ResumeCoroutine == null)
            {
                m_ResumeCoroutine = StartCoroutine(TrackEnterPressed());
            }
            else if (!isAllowed && m_ResumeCoroutine != null)
            {
                StopCoroutine(m_ResumeCoroutine);
                m_ResumeCoroutine = null;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="progress">Set parameter from 0f to 1f</param>
        public void SetLoadingProgress(float progress)
        {
            m_TargetProgress = progress >= m_FullProgress ? m_FullProgress : progress;

            if (m_SmoothProgressCoroutine == null)
            {
                m_SmoothProgressCoroutine = StartCoroutine(SetProgressSmoothly());
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

            if (IsProgressComplete)
            {
                OnProgressComplete?.Invoke();
            }

            m_SmoothProgressCoroutine = null;
        }

        private IEnumerator TrackEnterPressed()
        {
            while (true)
            {
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    SetButtonPressed();
                    yield break;
                }

                yield return null;
            }
        }

        private void SetButtonPressed()
        {
            m_ResumeCoroutine = null;
            m_PressButtonToContinueText.gameObject.SetActive(false);
            OnButtonPressed?.Invoke();
        }

        private void SetScreenCanvasActive(bool isActive)
        {
            m_LoadingScreenCanvas.gameObject.SetActive(isActive);
        }
    }
}
