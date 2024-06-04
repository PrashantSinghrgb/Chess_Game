using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace PrashantSingh.Utilities.ExtensionMethods
{
	/// <summary>A collection of image extention methods.</summary>
	public static class ImageExtensions 
    {
		/// <summary>Set the alpha of a Image.</summary>
		public static void SetAlpha(this Image image, float alpha)
		{
			Color temp = image.color;
			temp.a = alpha;
			image.color = temp;
		}
	}
}