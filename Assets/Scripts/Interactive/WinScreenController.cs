using DG.Tweening;
using LevelLoaderController;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Interactive.Detail
{
    public class WinScreenController : MonoBehaviour 
	{
        public event Action GuardianAnimationCompleted;
        public event Action ScreenAnimationCompleted;
        public event Action ButtonClicked;

        [SerializeField]
        protected GameObject animatedGuardian;

        [SerializeField]
        private CanvasGroup winScreen;

        [SerializeField]
        protected Transform finalGuardian;

        [SerializeField]
        protected float guardianAnimationDuration = 5;

        [SerializeField]
        private float screenAnimationDuration;

        [SerializeField]
        private Ease guardianAnimationEase;

        protected Sequence guardianSequence;

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

        public virtual void AnimateGuardian()
        {
            animatedGuardian.SetActive(true);
            guardianSequence = DOTween.Sequence();
            guardianSequence.Append(animatedGuardian.transform.DOMove(finalGuardian.position, guardianAnimationDuration).SetEase(guardianAnimationEase));
            guardianSequence.AppendCallback(OnGuardianAnimationComplete);
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