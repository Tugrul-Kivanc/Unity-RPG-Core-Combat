using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class Fader : MonoBehaviour
    {
        private CanvasGroup canvasGroup;
        private Coroutine currentFadeRoutine;
        private float fadeOutAlphaTarget = 1f;
        private float fadeInAlphaTarget = 0f;

        private void Awake()
        {
            canvasGroup = GetComponent<CanvasGroup>();
        }

        public Coroutine FadeOut(float time)
        {
            return Fade(fadeOutAlphaTarget, time);
        }

        public Coroutine FadeIn(float time)
        {
            return Fade(fadeInAlphaTarget, time);
        }

        public Coroutine Fade(float alphaTarget, float time)
        {
            if (currentFadeRoutine != null)
            {
                StopCoroutine(currentFadeRoutine);
            }

            currentFadeRoutine = StartCoroutine(FadeRoutine(alphaTarget, time));
            return currentFadeRoutine;
        }

        private IEnumerator FadeRoutine(float alphaTarget, float time)
        {
            while (!Mathf.Approximately(canvasGroup.alpha, alphaTarget))
            {
                canvasGroup.alpha = Mathf.MoveTowards(canvasGroup.alpha, alphaTarget, Time.deltaTime / time);
                yield return null;
            }
        }

        public void FadeOutImmediate()
        {
            canvasGroup.alpha = 1;
        }
    }
}
