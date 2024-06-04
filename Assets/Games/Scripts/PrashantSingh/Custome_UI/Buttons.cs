using System.Collections;
using UnityEngine;
using PrashantSingh.Utilities.ExtensionMethods;
using PrashantSingh.Utilities.Audio;
using UnityEngine.EventSystems;
using UnityEngine.UI;

// Included in the PrashantSingh.Custome_UI namespace
namespace PrashantSingh.Custome_UI
{
    /// <summary> A subclass of button which adds some additional functionality </summary>
    [RequireComponent(typeof(Image))]
    
    public class Buttons : Button
    {
        // Whether the button will scale on touch down events
        public bool scaleOnTouch = true;
        // Whether the button will play a sound on touch down events
        public bool sounOnTouch = true;

        // The buttons scale percentage
        [Range(0.5f, 1.5f)]
        public float scalePercentage = 1.05f;
        // The duration for a scale animation
        private const float _SCALE_DURATION = 0.05f;

        // The button soundfile key
        public AudioManagerKeys soundOnTouchKey = AudioManagerKeys.buttonGeneric;

        // the button alpha value
        public float alpha
        {
            get
            {
                return this.image.color.a;
            }
            set
            {
                this.image.SetAlpha(value);
            }
        }

        // The buttons color
        public Color color
        {
            get
            {
                return this.image.color;
            }
            set
            {
                this.image.color = value;
            }
        }

        // Set whether the button is active in the scene
        public void SetActive(bool active)
        {
            this.gameObject.SetActive(active);
        }

        // A call back when a touch down event has been registered
        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (interactable)
            {
                if (scaleOnTouch)
                {
                    StartCoroutine(transform.ScaleToInTime(scalePercentage, _SCALE_DURATION));
                }
                if (sounOnTouch)
                {
                    AudioManager.instance.PlaySFX(soundOnTouchKey);
                }
            }
        }

        // A callback when a touch up event has been registered
        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (interactable)
            {
                if (scaleOnTouch)
                {
                    StartCoroutine(transform.ScaleToInTime(1.0f, _SCALE_DURATION));
                }
            }
        }
    }
}