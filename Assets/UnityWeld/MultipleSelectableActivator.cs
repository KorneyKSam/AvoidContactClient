using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UnityWeld
{
    public class MultipleSelectableActivator : MonoBehaviour
    {
        [SerializeField]
        private List<Selectable> m_Selectables;

        public bool IsInteractable
        {
            get => m_Selectables.TrueForAll(g => g.IsInteractable());
            set => m_Selectables.ForEach(g => g.interactable = value);
        }
    }
}