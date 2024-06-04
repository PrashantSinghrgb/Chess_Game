using System.Collections;
using UnityEngine;
using PrashantSingh.Utilities;
using UnityEngine.Assertions;


namespace PrashantSingh.Custome_UI
{
    /// <summary>A group of DARadioButtons supporting single selection.</summary>
    public class RadioButtonGroup : MonoBehaviour
    {
		/// <summary>An event which is triggered when an index is selected.</summary>
		public EventHandlerInt OnIndexWasSelected;
		/// <summary>The group's radio buttons.</summary>
		[SerializeField] private RadioButton[] _radioButtons;
		/// <summary>The group's selected index.</summary>
		public int selectedIndex { get; private set; }

		/// <summary>Callback when the instance starts.</summary>
		private void Start()
		{
			Assert.IsTrue(_radioButtons.Length > 1);

			for (int i = 0; i < _radioButtons.Length; i++)
			{
				int temp = i; //a temporary local variable to pass to the closure
				_radioButtons[i].onClick.AddListener(() => {
					RadioButtonIsSelected(temp);
				});
			}
			selectedIndex = -1; //ResetForIndex must be called to initialize the buttons
		}

		/// <summary>Callback when the instance is being destroyed.</summary>
		private void OnDestroy()
		{
			for (int i = 0; i < _radioButtons.Length; i++)
			{
				_radioButtons[i].onClick.RemoveAllListeners();
			}
		}

		/// <summary>Reset the selected radio button to a given index.</summary>
		public void ResetForIndex(int index)
		{
			RadioButtonIsSelected(index);
		}

		/// <summary>Callback when a radio button is selected.</summary>
		public void RadioButtonIsSelected(int index)
		{
			Assert.IsTrue(index >= 0 && index < _radioButtons.Length);

			if (index != selectedIndex)
			{
				//update the selected index and trigger an event
				selectedIndex = index;
				if (OnIndexWasSelected != null) { OnIndexWasSelected(selectedIndex); }
				//update the group's buttons
				for (int i = 0; i < _radioButtons.Length; i++)
				{
					_radioButtons[i].SetSelected(selectedIndex == i);
				}
			}
		}
	}
}