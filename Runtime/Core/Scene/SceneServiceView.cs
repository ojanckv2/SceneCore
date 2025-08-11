using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

namespace Ojanck.Core.Scene
{
    [RequireComponent(typeof(CanvasGroup))]
    public class SceneServiceView : MonoBehaviour
    {
        private bool isActive = false;

        [SerializeField] protected CanvasGroup canvasGroup;
        private void OnValidate()
        {
            var hasCanvasGroup = TryGetComponent(out canvasGroup);
            if (!hasCanvasGroup)
            {
                Debug.LogWarning("CanvasGroup component is missing. Creating one automatically...");
                canvasGroup = gameObject.AddComponent<CanvasGroup>();
            }
        }

        [SerializeField] private bool dontHideOnActivate = true;
        public UnityEvent onShow = new();
        public UnityEvent onHide = new();

        public void Activate()
        {
            if (isActive) return;

            OnActivate();
            isActive = true;

            if (dontHideOnActivate == false)
            {
                SnapHide();
            }
        }

        public void Deactivate()
        {
            if (!isActive) return;

            SnapHide();
            OnDeactivate();

            isActive = false;
        }

        protected virtual void OnActivate() { }
        protected virtual void OnDeactivate() { }

        public void Show()
        {
            StartCoroutine(FadeIn());
        }

        public void Hide()
        {
            StartCoroutine(FadeOut());
        }

        private void SnapHide()
        {
            if (canvasGroup != null)
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
        }

        private IEnumerator FadeIn()
        {
            var duration = 0.25f;
            var endValue = 1f;
            var startValue = 0f;
            var elapsed = 0f;

            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
            onShow?.Invoke();

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                canvasGroup.alpha = Mathf.Lerp(startValue, endValue, t);
                yield return null;
            }
            canvasGroup.alpha = endValue;
        }

        private IEnumerator FadeOut()
        {
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
            onHide?.Invoke();

            var duration = 0.25f;
            var endValue = 0f;
            var startValue = canvasGroup.alpha;
            var elapsed = 0f;

            while (elapsed < duration)
            {
                elapsed += Time.deltaTime;
                var t = Mathf.Clamp01(elapsed / duration);
                canvasGroup.alpha = Mathf.Lerp(startValue, endValue, t);
                yield return null;
            }

            canvasGroup.alpha = endValue;
        }
    }
}