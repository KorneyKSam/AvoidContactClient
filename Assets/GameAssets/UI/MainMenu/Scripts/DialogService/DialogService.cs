using AdvancedDebugger;
using GOOfTpeAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using Tools.Debugging;
using UnityEngine;

namespace DialogBoxService
{
    public class DialogService : MonoBehaviour
    {
        public bool IsAnyDialogOpened => m_GODialogBoxes.Any(b => b.transform.gameObject.activeInHierarchy);

        private const float m_DefaultDuration = 0.2f;

        [SerializeField]
        private GameObject m_FadeOverlay;

        [SerializeField, GameObjectOfType(typeof(IDialogBox)), Tooltip("Game object has to implement " + nameof(IDialogBox))]
        private List<GameObject> m_GODialogBoxes;

        private List<IDialogBox> m_DialogBoxes = new();
        private int m_CountOfActiveDialogs => m_DialogBoxes.Count(d => d.IsActive);

        /// <summary>
        /// Open dialog in layer with duration.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dialogLayer">Opening different dialogs in one layer is not possible, old dialog will be closed.</param>
        /// <param name="duration"></param>
        /// <param name="onCompleteAnimation">A callback that will be called when animation will be fully completed.</param>
        /// <returns></returns>
        public T Open<T>(DialogLayer dialogLayer = DialogLayer.UIDialog1, float duration = m_DefaultDuration, Action onCompleteAnimation = null) where T : IDialogBox
        {
            if (TryToGetDialog<T>(out var foundedDialog))
            {
                if (foundedDialog.IsActive)
                {
                    Debugger.Log($"Dialog {typeof(T)} is already opened!", DebuggerLog.InfoWarning);
                }
                else
                {
                    if (TryToGetOpenedDialogInLayer(dialogLayer, out var openedDialog))
                    {
                        CloseDialog(openedDialog, duration, onCompleteAnimation: () =>
                        {
                            OpenDialogInLayer(foundedDialog, dialogLayer, duration, onCompleteAnimation);
                        });
                    }
                    else
                    {
                        OpenDialogInLayer(foundedDialog, dialogLayer, duration, onCompleteAnimation);
                    }
                }
                return foundedDialog;
            }
            else
            {
                onCompleteAnimation?.Invoke();
                return default;
            }
        }

        public T Close<T>(float duration = m_DefaultDuration, Action onCompleteAnimation = null) where T : IDialogBox
        {
            if (TryToGetDialog<T>(out var foundedDialog))
            {
                if (foundedDialog.IsActive)
                {
                    CloseDialog(foundedDialog, duration, onCompleteAnimation);
                }
                else
                {
                    Debugger.Log($"Attempting to close dialog {typeof(T)}! Dialog is not opened!", DebuggerLog.InfoWarning);
                }
                return foundedDialog;
            }
            else
            {
                onCompleteAnimation?.Invoke();
                return default;
            }
        }

        private void OpenDialogInLayer(IDialogBox dialog, DialogLayer dialogLayer, float duration, Action onCompleteAnimation)
        {
            dialog.SortingLayer = dialogLayer;
            dialog.Activate(true, duration, onCompleteAnimation);
            m_FadeOverlay.SetActive(true);
        }

        private void CloseDialog(IDialogBox dialogBox, float duration, Action onCompleteAnimation)
        {
            dialogBox.Activate(false, duration, () =>
            {
                if (m_CountOfActiveDialogs == 0)
                {
                    m_FadeOverlay.SetActive(false);
                }              
                onCompleteAnimation?.Invoke();
            });
        }

        private bool TryToGetDialog<T>(out T dialogBox) where T : IDialogBox
        {
            dialogBox = default;
            var findedDialog = m_DialogBoxes.FirstOrDefault(b => b is T);
            if (findedDialog != null)
            {
                dialogBox = (T)findedDialog;
                return true;
            }
            Debugger.Log($"There is no dialog {typeof(T)} in list!!!", DebuggerLog.InfoWarning);
            return false;
        }

        private bool TryToGetOpenedDialogInLayer(DialogLayer dialogLayer, out IDialogBox dialogBox)
        {
            foreach (var dialog in m_DialogBoxes)
            {
                if (dialog.IsActive && dialog.SortingLayer == dialogLayer)
                {
                    dialogBox = dialog;
                    return true;
                }
            }

            dialogBox = default;
            return false;
        }

        private void Awake()
        {
            if (m_GODialogBoxes != null)
            {
                m_GODialogBoxes.ForEach(b =>
                {
                    m_DialogBoxes.Add(b.GetComponent<IDialogBox>());
                    b.transform.localScale = Vector3.zero;
                });
            }
        }
    }
}