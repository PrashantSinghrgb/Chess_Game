using System.Collections;
using UnityEngine;

namespace PrashantSingh.Game
{
    [ExecuteInEditMode]
    // A script which auto updates the camera's orthographic size.
    public class LandscapeCameraOrthographicSizeAutoUpdate : MonoBehaviour
    {
        /// <summary>The target screen width.</summary>
		public int targetWidth = 1024;
		/// <summary>The number of pixels to units.</summary>
		public float pixelsToUnits = 1;

		#if UNITY_EDITOR
		private int _screenWidth, _screenHeight;
		#endif

		/// <summary>Awake the instance.</summary>
		private void Awake()
		{
			UpdateOrthographicSize();
			#if UNITY_EDITOR
			UpdateScreenVariables();
			#endif
		}

		/// <summary>Updates the camera's orthographic size.</summary>
		private void UpdateOrthographicSize()
		{
			int height = Mathf.RoundToInt(targetWidth / (float)Screen.width * Screen.height);
			Camera.main.orthographicSize = height / pixelsToUnits / 2;
		}

		#if UNITY_EDITOR
		/// <summary>Updates the instance.</summary>
		private void Update()
		{
			if(_screenWidth != Screen.width || _screenHeight != Screen.height)
			{
				UpdateOrthographicSize();
				UpdateScreenVariables();
			}
		}

		/// <summary>Updates the current screen variables.</summary>
		private void UpdateScreenVariables()
		{
			_screenWidth = Screen.width; _screenHeight = Screen.height;
		}
		#endif
    }
}