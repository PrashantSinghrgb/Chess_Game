using System.Collections;
using UnityEngine;

namespace PrashantSingh.Utilities
{
	/// <summary>A generic Event handler which takes no parameters.</summary>
	public delegate void EventHandler();

	/// <summary>A generic Event handler which takes an int parameter.</summary>
	public delegate void EventHandlerInt(int value);

	/// <summary>A generic Event handler which takes a bool parameter.</summary>
	public delegate void EventHandlerBool(bool value);
}