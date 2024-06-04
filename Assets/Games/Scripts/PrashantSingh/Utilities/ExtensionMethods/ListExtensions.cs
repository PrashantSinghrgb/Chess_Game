using System.Collections.Generic;
using Random = UnityEngine.Random;


namespace PrashantSingh.Utilities.ExtensionMethods
{
    /// <summary>A collection of list extention methods.</summary>
    public static class ListExtensions 
    {
		/// <summary>Shuffle a list.</summary>
		public static void Shuffle<T>(this IList<T> list)
		{
			int n = list.Count;
			while (n > 1)
			{
				n--;
				int k = Random.Range(0, n + 1);
				T value = list[k];
				list[k] = list[n];
				list[n] = value;
			}
		}
	}
}