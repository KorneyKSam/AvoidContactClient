using System.Collections.Generic;
using UnityEngine;

namespace UnityWeld
{
    public class GOActivatorCrutch : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> m_GameObjects;

        public bool IsActive
        {
            get => m_GameObjects.TrueForAll(g => g.activeInHierarchy);
            set => m_GameObjects.ForEach(g => g.SetActive(value));
        }
    }
}