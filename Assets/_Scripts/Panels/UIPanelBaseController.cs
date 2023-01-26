using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace UIPanels
{
    /// <summary>
    /// Базовый класс-контроллер панели GUI,
    /// Содержит методы и корутины Show/Hide
    /// для реализации анимации появления/исчезновения
    /// </summary>
    public abstract class UIPanelBaseController : MonoBehaviour
    {
        [SerializeField]
        protected Toggle toggle;
        public Button closeButton;
        
        public UnityEvent OnShowStart;
        public UnityEvent OnCloseStart;
        public UnityEvent OnShowEnd;
        public UnityEvent OnCloseEnd;
        public float animationDuration = 0.25f;
        
        private bool _isAnimating = false;
        public void Show()
        {
            StopAnimation();
            animationRoutine = StartCoroutine(ShowPanelRoutine());
        }

        public void Hide()
        {
            StopAnimation();
            animationRoutine = StartCoroutine(HidePanelRoutine());
        }

        public Coroutine animationRoutine;
        public abstract IEnumerator ShowPanelRoutine();
        public abstract IEnumerator HidePanelRoutine();

        private void StopAnimation()
        {
            if (_isAnimating)
            {
                if(animationRoutine != null)
                    StopCoroutine(animationRoutine);
                _isAnimating = false;
            }
        }
    }
}
