using System.Linq;
using UnityEngine;

namespace Ojanck.Core
{
    public class SceneCoreView : SceneService
    {
        private static SceneCoreView Instance;
        [SerializeField] private SceneServiceView[] sceneServiceViews;

        protected override void OnInitialize()
        {
            base.OnInitialize();

            Instance = this;

            FetchSceneServiceViews();
            ActivateViews();
        }

        protected override void OnActivate()
        {
            base.OnActivate();
        }

        protected override void OnDeactivate()
        {
            base.OnDeactivate();

            DeactivateViews();
        }

        private void FetchSceneServiceViews()
        {
            sceneServiceViews = transform.GetComponentsInChildren<SceneServiceView>(true);
        }

        private void ActivateViews()
        {
            foreach (var view in sceneServiceViews)
            {
                view.Activate();
            }
        }

        private void DeactivateViews()
        {
            foreach (var view in sceneServiceViews)
            {
                view.Deactivate();
            }
        }
        
        public static T GetSceneServiceView<T>() where T : SceneServiceView
        {
            if (Instance == null) {
                Debug.LogError("SceneCoreView does not exist in the scene.");
                return null;
            }

            return Instance.sceneServiceViews.FirstOrDefault(view => view is T) as T;
        }
    }
}