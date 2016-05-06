using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;
using System;
using LevelLoaderController;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Interactive.Detail
{
	public class WinScreenBossController : WinScreenController 
	{
        public event Action FusionAnimationCompleted;

        [SerializeField]
        private Transform[] extraGuardians;

        [SerializeField]
        private float extraAnimationsStartSecond;

        [SerializeField]
        private Ease extraGuardiansEase;

        [SerializeField]
        private Ease fusionGuardiansEase;

        [SerializeField]
        private float fusionDuration;

        [SerializeField]
        private Image overlay;

        public override void AnimateGuardian()
        {
            base.AnimateGuardian();

            for (int i = 0; i < extraGuardians.Length; i++)
            {
                guardianSequence.Insert(extraAnimationsStartSecond, extraGuardians[i].
                    DOMoveY(finalGuardian.position.y, guardianAnimationDuration - extraAnimationsStartSecond).
                    SetEase(extraGuardiansEase));
            }

        }

        public void FusionGuardians()
        {
            Sequence fusionSequence = DOTween.Sequence();

            for (int i = 0; i < extraGuardians.Length; i++)
            {
                fusionSequence.Insert(0, extraGuardians[i].DOMoveX(finalGuardian.position.x, fusionDuration).SetEase(fusionGuardiansEase));
            }

            fusionSequence.Append(overlay.DOFade(1, 0.5f));
            fusionSequence.AppendInterval(0.5f);
            fusionSequence.AppendCallback(OnFusionAnimationComplete);
        }

        private void OnFusionAnimationComplete()
        {
            DeactivateObjects(); 

            if (FusionAnimationCompleted != null)
                FusionAnimationCompleted();

            FusionAnimationCompleted = null;

        }

        private void DeactivateObjects()
        {
            overlay.gameObject.SetActive(false);

            for (int i = 0; i < extraGuardians.Length; i++)
            {
                extraGuardians[i].gameObject.SetActive(false);
            }

            animatedGuardian.SetActive(false);

        }


    }
}