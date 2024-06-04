using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PrashantSingh.Utilities.Serialization;
using PrashantSingh.Utilities.ExtensionMethods;
using Utilities;

namespace Scenes
{
    public class LoadingScene : MonoBehaviour
    {
        // DEBUG
        [SerializeField]
        private GameObject _resetButton;
        // DEBUG

        // Callback when the Instance is awoken
        private void Awake()
        {
            //if the game hasn't been previously launced, create and set initial data
            if (!PlayerPreferences.HasKey(PlayerPreferencesKeys.perviouslyLaunched))
            {
                DataManager.Initalize();
            }
            DataManager.Verify();
            //load game scene - doesn't use cached yields
            this.Invoke(action: () => {
                SceneManager.LoadScene(SceneBuildIndeces.MenuScene);
            }, time: AnimationDuration.LOADING_SCENE, useCachedWaits: false);
        }

        public void ResetButtonPressed()
        {
            DataManager.ReloadData();
            _resetButton.SetActive(false);
        }
    }
}
