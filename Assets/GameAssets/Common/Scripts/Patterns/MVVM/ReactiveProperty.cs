using System;

namespace Common
{
    public class ReactiveProperty<T>
    {
        public event Action<T> OnChanged;

        public T Value
        {
            get => m_Value;
            set
            {
                m_Value = value;
                OnChanged?.Invoke(m_Value);
            }
        }

        private T m_Value;
    }
}