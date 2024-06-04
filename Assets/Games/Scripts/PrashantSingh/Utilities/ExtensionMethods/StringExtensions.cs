using System.Collections;
using UnityEngine;

namespace PrashantSingh.Utilities.ExtensionMethods
{
	/// <summary>A collection of string extention methods.</summary>
	public static class StringExtensions 
    {
		/// <summary>Determines if the string is null or empty.</summary>
		/// <returns><c>true</c> if the string is null or empty, otherwise <c>false</c>.</returns>
		public static bool IsNullOrEmpty(this string value)
		{
			return string.IsNullOrEmpty(value);
		}
	}
}