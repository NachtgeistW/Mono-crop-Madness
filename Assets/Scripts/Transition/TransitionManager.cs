using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Transition
{
    public class TransitionManager : MonoBehaviour
    {
        [SceneName]
        public string startSceneName = string.Empty;
        private CanvasGroup fadeCanvasGroup;
        private bool isFade;

        private void OnEnable()
        {
            EventHandler.TransitionEvent += OnTransitionEvent;
        }

        private void OnDisable()
        {
            EventHandler.TransitionEvent -= OnTransitionEvent;
        }

        private IEnumerator Start()
        {
            fadeCanvasGroup = FindObjectOfType<CanvasGroup>();
            yield return LoadSceneAndActivate(startSceneName);
            EventHandler.CallAfterSceneLoadedEvent();
        }

        private void OnTransitionEvent(string sceneName, Vector3 pos)
        {
            if (!isFade)
                StartCoroutine(Transition(sceneName, pos));
        }

        /// <summary>
        /// Load scene and set it as activate
        /// </summary>
        /// <param name="sceneName">scene to be loaded</param>
        /// <returns></returns>
        private IEnumerator LoadSceneAndActivate(string sceneName)
        {
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
            Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
            SceneManager.SetActiveScene(newScene);
        }

        /// <summary>
        /// Unload the current scene and switch to another scene
        /// </summary>
        /// <param name="sceneName">scene to be loaded</param>
        /// <param name="targetPos">position</param>
        /// <returns></returns>
        private IEnumerator Transition(string sceneName, Vector3 targetPos)
        {
            EventHandler.CallBeforeSceneUnloadedEvent();
            yield return Fade(1);
            yield return SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene());

            yield return LoadSceneAndActivate(sceneName);
            EventHandler.CallMoveToPositionEvent(targetPos);
            EventHandler.CallAfterSceneLoadedEvent();
            yield return Fade(0);
        }

        /// <summary>
        /// Scene fade in or fade out
        /// </summary>
        /// <param name="targetAlpha">1 stands for fully show, 0 for hidden</param>
        /// <returns></returns>
        private IEnumerator Fade(float targetAlpha)
        {
            isFade = true;
            fadeCanvasGroup.blocksRaycasts = true;
            
            //TODO: replace with DOTween
            float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / Settings.sceneFadeDuration;
            while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
            {
                fadeCanvasGroup.alpha = Mathf.MoveTowards(fadeCanvasGroup.alpha, targetAlpha, speed * Time.deltaTime);
                yield return null;
            }
            
            fadeCanvasGroup.blocksRaycasts = false;
            isFade = false;
        }
    }
}
