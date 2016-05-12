using DG.Tweening;
using LevelLoaderController;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactive.Detail
{
    public class EndGameScreenController : MonoBehaviour 
	{
        public event Action ButtonClicked;

        public void RestartGame()
        {
            OnButtonClicked();
            LevelLoader.Instance.LoadScene(SceneManager.GetActiveScene().name);
        }

        public void LoadNextLevel()
        {
            OnButtonClicked();
            LevelLoader.Instance.LoadNextLevel();
        }

        public void LoadAreaSelection()
        {
            OnButtonClicked();
            LevelLoader.Instance.LoadScene(SceneProperties.SCENE_MAIN_MENU);
        }

        public void LoadLevelSelection()
        {
            OnButtonClicked();
            LevelLoader.Instance.LoadScene(SceneProperties.SCENE_LOADER_AREA);
        }


        private void OnButtonClicked()
        {
            if (ButtonClicked != null)
                ButtonClicked();

            ButtonClicked = null;
		}
    }
}