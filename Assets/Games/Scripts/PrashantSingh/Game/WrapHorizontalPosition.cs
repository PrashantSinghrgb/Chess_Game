using System.Collections;
using UnityEngine;

namespace PrashantSingh.Game
{
    /// This script wraps the position of an object to its start position after moving a given width.
    public class WrapHorizontalPosition : MonoBehaviour
    {
		/// <summary>The object's total width.</summary>
		[Tooltip("The object's total width.")]
		[SerializeField] private float _totalWidth;
		/// <summary>The object's start position.</summary>
		private Vector3 _startPosition;

		/// <summary>Callback when the object awakes.</summary>
		private void Awake()
		{
			_startPosition = transform.position;
		}

		/// <summary>Callback once per frame when the object updates.</summary>
		private void Update()
		{
			//if object is offscreen, wrap back to start position
			if (Mathf.Abs(transform.position.x - _startPosition.x) >= _totalWidth)
			{
				transform.position = _startPosition;
			}
		}
	}
}