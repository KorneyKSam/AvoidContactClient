using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class InputSelection : MonoBehaviour
    {
        [SerializeField]
        private List<TMP_InputField> m_InputFields;

        private int m_FieldIndex;

        private void OnEnable()
        {
            m_InputFields.ForEach(field => field.onSelect.AddListener(OnFieldSelect));
            InvokeRepeating(nameof(CheckTabFocusKeys), 0f, 0.1f);
        }

        private void OnDisable()
        {
            m_InputFields.ForEach(field => field.onSelect.RemoveListener(OnFieldSelect));
            CancelInvoke(nameof(CheckTabFocusKeys));
        }

        private void OnFieldSelect(string arg)
        {
            UpdateSelectIndex();
        }

        private void UpdateSelectIndex()
        {
            var currentSelectedObject = EventSystem.current.currentSelectedGameObject;
            for (int i = 0; i < m_InputFields.Count; i++)
            {
                if (currentSelectedObject == m_InputFields[i].gameObject)
                {
                    m_FieldIndex = i;
                    return;
                }
            }
        }

        private void CheckTabFocusKeys()
        {
            if (Input.GetKey(KeyCode.Tab))
            {
                int index = 0;
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    index = m_FieldIndex - 1 <= -1 ? m_InputFields.Count - 1 : m_FieldIndex - 1;
                }
                else
                {
                    index = m_FieldIndex + 1 >= m_InputFields.Count ? 0 : m_FieldIndex + 1;
                }
                m_InputFields[index].Select();
            }
        }
    }
}