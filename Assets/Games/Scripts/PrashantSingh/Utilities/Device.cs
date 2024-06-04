using System.Collections;
using UnityEngine;

namespace PrashantSingh.Utilities
{
	/// <summary>A static class containing helper functions to determine particulars of the Device.</summary>
	public static class Device
	{
		/// <summary>Determine if the device has internet connectivity.</summary>
		public static bool hasInternetConnection
		{
			get { return Application.internetReachability != NetworkReachability.NotReachable; }
		}

		/// <summary>Determine if the device has a wifi connectivity.</summary>
		public static bool hasWifiConnection
		{
			get { return Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork; }
		}
	}
}