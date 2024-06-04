using System.Collections;
using UnityEngine;
using PrashantSingh.Utilities.ExtensionMethods;

namespace PrashantSingh.Game
{
    // Transform's y-positions matches the camera's orthographic size. Useful for ceiling gameobject colliders.
    public class MatchOrthographicSize : MonoBehaviour
    {
        /// <summary>Callback when the object starts.</summary>
		private void Start()
        {
            transform.SetY(Camera.main.orthographicSize);
        }
    }
}