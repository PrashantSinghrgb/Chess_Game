using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using Models;

namespace PrashantSingh.Custome_UI
{
    public class SettingsManagerBooleanToggleButton : ToggleButton
    {
		/// <summary>The button's settings variable.</summary>
		[SerializeField] private SettingsManager.BooleanVariable settingsVariable;

		/// <summary>Callback when the instance is started.</summary>
		new private void Start()
		{
			//first determine if the setting is selected or not, then call base class
			selected = SettingsManager.instance.GetBooleanVariableValue(settingsVariable);
			base.Start();
		}

		/// <summary>A callback when a touch down event has been registered.</summary>
		public override void OnPointerDown(PointerEventData eventData)
		{
			//pass event data onto base class
			base.OnPointerDown(eventData);
			//if the button is interactable, send selected state onto SettingsManager
			if (interactable) { SettingsManager.instance.ToggleBooleanVariableValue(settingsVariable); }
		}
	}
}