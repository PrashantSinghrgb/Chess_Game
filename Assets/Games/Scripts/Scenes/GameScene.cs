using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Scenes
{
    public class GameScene : MonoBehaviour
    {
        #region CallBacks

        // Callback when the exit button is Pressed
        public void ExitButtonPressed()
        {
            SceneManager.LoadScene(SceneBuildIndeces.MenuScene);
        }

        #endregion
    }
}
