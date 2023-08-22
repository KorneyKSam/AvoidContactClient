using DG.Tweening;
using DialogBoxService;
using System;
using System.Collections.Generic;
using UI.ViewModels;
using UnityEngine;

namespace UI.DialogBoxes
{
    public class PersonalFileDialog : ZoomedDialog
    {
        public event Action OnCancelClick;
        [Header("Side positions")]
        [SerializeField]
        private Transform m_LeftSideTransform;

        [SerializeField]
        private Transform m_RightSideTransform;

        [Header("View models (Sheets)")]
        [SerializeField]
        private RegistrationViewModel m_RegistrationViewModel;

        [SerializeField]
        private PlayerInfoViewModel m_PlayerInfoViewModel;

        private Dictionary<PersonalFileSheet, GameObject> m_Sheets;
        private Dictionary<FileSide, Vector2> m_Positions;

        public override void Init()
        {
            m_Sheets = GetSheets();
            m_Positions = GetSheetPositions();
            base.Init();
        }

        public override void Activate(bool isActive, float duration, Action onCompleteAnimation = null)
        {
            RemoveListeners();

            if (isActive)
            {
                AddListeners();
            }

            base.Activate(isActive, duration, onCompleteAnimation);
        }

        public void ActivateFileSheets(bool isActive, params PersonalFileSheet[] sheets)
        {
            foreach (var sheet in sheets)
            {
                ActivateFileSheet(sheet, isActive);
            }
        }

        public void ActivateFileSheet(PersonalFileSheet personalFileSheet, bool isActive)
        {
            m_Sheets[personalFileSheet].SetActive(isActive);
        }

        public void MoveSheet(PersonalFileSheet personalFileSheet, FileSide side, Action onComplete = null, float duration = 0.3f)
        {
            m_Sheets[personalFileSheet].transform.DOLocalMove(m_Positions[side], duration).OnComplete(() => onComplete?.Invoke());
        }

        private void AddListeners()
        {
            m_RegistrationViewModel.CancelRegistration.onClick.AddListener(CancelPersonalFile);
        }

        private void RemoveListeners()
        {
            m_RegistrationViewModel.CancelRegistration.onClick.RemoveListener(CancelPersonalFile);
        }

        private void CancelPersonalFile()
        {
            OnCancelClick?.Invoke();
        }


        private Dictionary<PersonalFileSheet, GameObject> GetSheets()
        {
            return new Dictionary<PersonalFileSheet, GameObject>()
            {
                { PersonalFileSheet.Registration, m_RegistrationViewModel.gameObject },
                { PersonalFileSheet.PlayerInfo, m_PlayerInfoViewModel.gameObject },
            };
        }

        private Dictionary<FileSide, Vector2> GetSheetPositions()
        {
            return new Dictionary<FileSide, Vector2>()
            {
                { FileSide.Left, m_LeftSideTransform.localPosition },
                { FileSide.Right, m_RightSideTransform.localPosition },
            };
        }
    }
}