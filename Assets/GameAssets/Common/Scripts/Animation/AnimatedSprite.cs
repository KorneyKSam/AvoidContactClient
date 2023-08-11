using System;
using System.Collections;
using UnityEngine;

namespace Common.Animation
{
    [RequireComponent(typeof(Animator))]
    public class AnimatedSprite : MonoBehaviour
    {
        public bool IsPlaying => m_Animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f;

        [SerializeField]
        private Animator m_Animator;
        private Coroutine m_Coroutine;
        private Action m_OnCompleteCallback;

        public void PlayAnimation(string animationName, Action onCompleteCallback = null)
        {
            m_OnCompleteCallback = onCompleteCallback;
            m_Animator.Play(animationName);

            if (m_Coroutine != null)
            {
                StopCoroutine(m_Coroutine);
                m_Coroutine = null;
            }
            if (m_OnCompleteCallback != null)
            {
                m_Coroutine = StartCoroutine(InvokeCallbackOnClipComplete(animationName));
            }
        }

        private IEnumerator InvokeCallbackOnClipComplete(string animationName)
        {
            while (!CheckIfAnimationPlaying(animationName))
            {
                yield return null;
            }

            while (IsPlaying)
            {
                yield return null;
            }
            m_OnCompleteCallback?.Invoke();
        }

        private bool CheckIfAnimationPlaying(string animationName)
        {
            return m_Animator.GetCurrentAnimatorStateInfo(0).IsName(animationName);
        }

        private void OnValidate()
        {
            m_Animator ??= GetComponent<Animator>();
        }
    }
}