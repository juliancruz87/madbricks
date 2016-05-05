using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;
using System;
using LevelLoaderController;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Interactive.Detail
{
	public class WinScreenController : MonoBehaviour 
	{
        public event Action GuardianAnimationCompleted;
        public event Action ScreenAnimationCompleted;
        public event Action ButtonClicked;

        [SerializeField]
        private GameObject animatedGuardian;

        [SerializeField]
        private CanvasGroup winScreen;

        [SerializeField]
        private Transform finalGuardian;

        [SerializeField]
        private float guardianAnimationDuration = 5;

        [SerializeField]
        private float screenAnimationDuration;

        private void Start()
        {
            animatedGuardian.SetActive(false);
        }

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

        public void AnimateGuardian()
        {
            animatedGuardian.SetActive(true);
            Sequence sequence = DOTween.Sequence();
            sequence.Append(animatedGuardian.transform.DOMove(finalGuardian.position, guardianAnimationDuration));
            sequence.AppendCallback(OnGuardianAnimationComplete);
        }

        public void AnimateWinScreen()
        {
            winScreen.blocksRaycasts = true;
            winScreen.DOFade(1, screenAnimationDuration).OnComplete(OnScreenAnimationComplete); ;
        }

        private void OnButtonClicked()
        {
            if (ButtonClicked != null)
                ButtonClicked();

            ButtonClicked = null;
        }

        private void OnGuardianAnimationComplete()
        {
            if (GuardianAnimationCompleted != null)
                GuardianAnimationCompleted();

            GuardianAnimationCompleted = null;

        }

        private void OnScreenAnimationComplete()
        {
            if (ScreenAnimationCompleted != null)
                ScreenAnimationCompleted();

            ScreenAnimationCompleted = null;
        }

    }
}