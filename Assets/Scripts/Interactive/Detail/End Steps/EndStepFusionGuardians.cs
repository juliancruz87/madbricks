using System;
using UnityEngine;
using System.Collections;
using Sound;
using DG.Tweening;
using UnityEngine.UI;

namespace Interactive.Detail
{
	public class EndStepFusionGuardians : BeginStepGameBase
	{

		[SerializeField]
		protected GameObject animatedGuardian;

		[SerializeField]
		protected Transform finalGuardian;

		[SerializeField]
		private Transform[] extraGuardians;

		[SerializeField]
		private Ease fusionEase;

		[SerializeField]
		private float fusionDuration;

		[SerializeField]
		private Image overlay;

		[SerializeField]
		private float overlayFadeStart;

		[SerializeField]
		private float overlayFadeDuration;

		[SerializeField]
		private Ease overlayFadeEase;

		[SerializeField]
		private float endDelay;

		[SerializeField]
		private float overlayExitDuration;

		[SerializeField]
		private Ease overlayExitEase;

		public override void StartStep()
		{
			Sequence fusionSequence = DOTween.Sequence();

			for (int i = 0; i < extraGuardians.Length; i++)
			{
				fusionSequence.Insert(0, extraGuardians[i].DOMoveX(finalGuardian.position.x, fusionDuration).SetEase(fusionEase));
			}

			fusionSequence.Insert(overlayFadeStart, overlay.DOFade(1, overlayFadeDuration).SetEase(overlayFadeEase));

			fusionSequence.AppendInterval(endDelay);
			fusionSequence.AppendCallback(OnFusionAnimationComplete);
			fusionSequence.Append (overlay.DOFade (0, overlayExitDuration).SetEase(overlayExitEase));
		}

		private void DeactivateObjects()
		{
			for (int i = 0; i < extraGuardians.Length; i++)
			{
				extraGuardians[i].gameObject.SetActive(false);
			}

			animatedGuardian.SetActive(false);
		}

		private void OnFusionAnimationComplete()
		{
			DeactivateObjects();
			EndStep ();
		}
	}
}