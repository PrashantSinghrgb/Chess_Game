using System.Collections;
using UnityEngine;
using UnityEngine.Assertions;

// Included in the PrashantSingh.Cutome_UI namespace
namespace PrashantSingh.Custome_UI
{
    // A custom component added to a ui canvas which allows the canvas to be faded in and out, interactability updated etc
    [RequireComponent(typeof(CanvasGroup))]
    public class Canvas : MonoBehaviour
    {
        // the canvas group
        private CanvasGroup _canvasGroup;

        // callback when the object is awoken
        private void Awake()
        {
            // get a reference to the gameobject canvasgroup
            _canvasGroup = GetComponent<CanvasGroup>();
            SetVisibleInteractable(false);
            _canvasGroup.ignoreParentGroups = false;
        }

        // sets whether the canvas is interactable
        public void SetInteractable(bool interactable)
        {
            // interactability of all children
            _canvasGroup.interactable = interactable;

            // whether touches will be processed
            _canvasGroup.blocksRaycasts = interactable;
        }

        // Determine if the canvas is visible
        public bool IsVisible
        {
            get
            {
                return _canvasGroup.alpha != 0;
            }
        }

        // Sets whether the panel is interactable
        public void SetVisibility(float alpha)
        {
            Assert.IsTrue(alpha >= 0 && alpha <= 1);

            _canvasGroup.alpha = alpha;
        }

        // Sets whether the panel is visible
        public void SetVisible(bool isVisible)
        {
            _canvasGroup.alpha = (isVisible ? 1f : 0f);
        }

        // Sets whether the panel is visible and interactable
        public void SetVisibleInteractable(bool isVisibleInteractable)
        {
            SetVisible(isVisibleInteractable);
            SetInteractable(isVisibleInteractable);
        }

        // Fade the canvas in with a given duration
        /// <param name="duration">Duration</param>
        public void FadeInWithDuration(float duration)
        {
            _canvasGroup.alpha = 0;
            StartCoroutine(FadeToAlphaWithDuration(1f, duration));
        }

        // Fade the canvas out with a given duration
        /// <param name="duration">Duration</param>
        public void FadeOutWithDuration(float duration)
        {
            StartCoroutine(FadeToAlphaWithDuration(0f, duration));
        }

        // Fade the canvas in with a given duration
        /// <param name="duration">Duration</param>
        /// <param name="alpha">Alpha</param>
        private IEnumerator FadeToAlphaWithDuration(float alpha, float duration)
        {
            Assert.IsTrue(alpha >= 0 && alpha <= 1);
            Assert.IsNotNull(_canvasGroup);

            // Canvas is not interactable for duration of fading
            SetInteractable(false);

            float speed = 1f / duration;
            float fadeUpDown = alpha > _canvasGroup.alpha ? 1 : -1;

            while (_canvasGroup.alpha != alpha)
            {
                _canvasGroup.alpha += fadeUpDown * Time.deltaTime * speed;
                // yield until after update of next frame
                yield return null;
            }

            if (IsVisible)
            {
                // if canvas is visible, it is once-again interactable
                SetInteractable(true);
            }
        }
    }
}