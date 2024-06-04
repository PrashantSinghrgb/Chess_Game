#if UNITY_EDITOR
using System.Collections;
using UnityEngine;
using PrashantSingh.Utilities;
using PrashantSingh.Utilities.Singleton;
namespace PrashantSingh.Game
{
    //A Keyboard controller used in Editor mode to listen for certain keypressed.
	// Objects (i.e. PlayerController) can suscribe to these events.
    public class KeyboardController : MonoSingleton<KeyboardController>
    {
		/// <summary>An event which is triggered when the spacebar is pressed.</summary>
		public EventHandler OnSpacebar;

		/// <summary>Callback once per frame.</summary>
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.Space))
			{
				if (OnSpacebar != null) { OnSpacebar(); }
			}
		}
	}
}
#endif