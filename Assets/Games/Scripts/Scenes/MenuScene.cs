using System.Collections;
using UnityEngine;
using PrashantSingh.Utilities.Serialization;
using PrashantSingh.Custome_UI;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using Utilities;

namespace Scenes
{
    // this script controls the menu scene
    public class MenuScene : MonoBehaviour
    {
        #region Properties

        // Player 1 radio button group
        [SerializeField]
        private RadioButtonGroup _player1RadioButtonGroup;
        // Player 2 radio button group
        [SerializeField]
        private RadioButtonGroup _player2RadioButtonGroup;
        // Play button 
        [SerializeField]
        private Buttons _playButton;

        #endregion

        #region Initialization

        //Edition Only: Callback when the script is loaded or value is changed in the inspector
        private void OnValidate()
        {
            Assert.IsNotNull(_player1RadioButtonGroup);
            Assert.IsNotNull(_player2RadioButtonGroup);
            Assert.IsNotNull(_playButton);
        }

        // Callback when the instance starts
        private void Start()
        {
            // Initialize the radio button group
            _player1RadioButtonGroup.ResetForIndex(PlayerPreferences.GetInt(PlayerPreferencesKeys.player1));
            _player2RadioButtonGroup.ResetForIndex(PlayerPreferences.GetInt(PlayerPreferencesKeys.player2));
        }

        #endregion

        #region CallBacks

        // Callback when the PlayButton is Pressed
        public void PlayButtonPressed()
        {
            _playButton.interactable = false;

            // Update Player1 and Player 2 Preferences and load the Game Scene
            PlayerPreferences.SetInt(PlayerPreferencesKeys.player1, _player1RadioButtonGroup.selectedIndex);
            PlayerPreferences.SetInt(PlayerPreferencesKeys.player2, _player2RadioButtonGroup.selectedIndex);
            SceneManager.LoadScene(SceneBuildIndeces.GameScene);
        }

        #endregion
    }
}