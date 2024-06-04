using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


namespace PrashantSingh.Custome_UI
{
    /// <summary>A subclass of Button which adds some additional functionality.</summary>
	[RequireComponent(typeof(Image))]
    public class ToggleButton : Buttons
    {
		/// <summary>A sprite for the off state.</summary>
		[SerializeField] private Sprite _offSprite;
		/// <summary>A sprite for the on state.</summary>
		[SerializeField] private Sprite _onSprite;

		/// <summary>A backing variable for selected.</summary>
		private bool _selected;
		/// <summary>Whether the toggle is selected (i.e. on).</summary>
		public bool selected
		{
			get { return _selected; }
			set { _selected = value; Refresh(); }
		}

		/// <summary>Callback when the instance is started.</summary>
		new private void Start()
		{
			//call base implementation and refresh the toggle
			base.Start();
			Refresh();
		}

		/// <summary>Refreshes the toggle.</summary>
		private void Refresh()
		{
			image.sprite = selected ? _onSprite : _offSprite;
		}

		/// <summary>A callback when a touch down event has been registered.</summary>
		public override void OnPointerDown(PointerEventData eventData)
		{
			//pass event data onto base class
			base.OnPointerDown(eventData);
			//if the button is interactable, toggle selected state
			if (interactable) { selected = !selected; }
		}
	}
}