using System.Collections;
using UnityEngine;

// Included in the PrashantSingh.Cutome_UI namespace
namespace PrashantSingh.Custome_UI
{
    // A simple RadioButton which is extended from unity ui button
    // This class attidional properties are viewable in the inspector via RadioButtonEditor script
    public class RadioButton : Buttons
    {
        // whether the button is selected or not
        public bool isSelected
        {
            get;
            private set;
        }

        [SerializeField]
        private Sprite _selectedSprite;
        [SerializeField]
        private Sprite _unSelectedSprite;

        // when the button is selected, the sprite outline is displayed
        public void SetSelected(bool selected)
        {
            isSelected = selected;
            image.sprite = selected ? _selectedSprite : _unSelectedSprite;
        }
    }
}