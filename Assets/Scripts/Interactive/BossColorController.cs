using UnityEngine;
using DG.Tweening;
using UnityStandardAssets.ImageEffects;
using System;

namespace Interactive.Detail
{
	public class BossColorController : MonoBehaviour 
	{
        public event Action AnimationCompleted;

        [SerializeField]
        private GameObject secondaryCameraObject;

		[SerializeField]
        private ColorCorrectionCurves colorEffect;

        [SerializeField]
        private float transitionTime = 5;

        private bool isInGreyScale;

        private ITotem boss;
        

        private void Start()
        {
            secondaryCameraObject.SetActive(true);
            colorEffect.saturation = 0;
            GameManager.Instance.TotemsSet += SetBoss;
            isInGreyScale = true;
        }

        private void SetBoss()
        {
            boss = GameManager.Instance.Boss;
        }

        private void Update()
        {
            if (boss != null && boss.IsJailed && isInGreyScale)
            {
                isInGreyScale = false;
                DOTween.To(() => colorEffect.saturation, x => colorEffect.saturation = x, 1, transitionTime).OnComplete(OnAnimationComplete);
            }
        }

        private void OnAnimationComplete()
        {
            secondaryCameraObject.SetActive(false);

            if (AnimationCompleted != null)
                AnimationCompleted();

            AnimationCompleted = null;
        }

	}
}