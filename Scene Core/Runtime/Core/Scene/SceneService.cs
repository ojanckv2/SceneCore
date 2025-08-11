using UnityEngine;

namespace Ojanck.Core
{
    public abstract class SceneService : MonoBehaviour
    {
        private bool isInitialized = false;
        private bool isActive = false;

        public void Initialize()
        {
            if (isInitialized) return;

            OnInitialize();
            isInitialized = true;
        }

        public void DeInitialize()
        {
            if (!isInitialized) return;

            OnDeInitialize();
            isInitialized = false;
        }

        public void Activate()
        {
            if (!isInitialized) return;
            if (isActive) return;

            OnActivate();
            isActive = true;
        }

        public void Deactivate()
        {
            if (!isInitialized) return;
            if (!isActive) return;

            OnDeactivate();
            isActive = false;
        }

        protected virtual void OnInitialize() { }
        protected virtual void OnDeInitialize() { }
        protected virtual void OnActivate() { }
        protected virtual void OnDeactivate() { }
    }
}