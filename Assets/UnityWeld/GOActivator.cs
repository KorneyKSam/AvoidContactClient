using System.Collections.Generic;
using UnityEngine;

namespace UnityWeld
{
    public class GOActivator : MonoBehaviour
    {
        [SerializeField]
        private List<ActivatedGameObject> m_ActivatedElements;

        public bool IsActive
        {
            get => m_IsActive;
            set
            {
                m_IsActive = value;
                m_ActivatedElements.ForEach(e => e.GameObject.SetActive(e.IsInversed ? !m_IsActive : m_IsActive));
            }
        }

        private bool m_IsActive;
    }
}