using System;

namespace DialogBoxService
{
    public interface IDialogBox
    {
        public bool IsActive { get; }
        public DialogLayer SortingLayer { get; set; }

        public void Activate(bool isActive, float duration, Action onCompleteAnimation = null);
    }
}