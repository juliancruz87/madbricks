using System;
using UnityEngine;
using System.Collections;
using Sound;
using DG.Tweening;

namespace Interactive.Detail
{
	public class EndStepAnimateGuardians : BeginStepGameBase
	{
		[SerializeField]
		protected GameObject animatedGuardian;

		[SerializeField]
		protected Transform finalGuardian;

		[SerializeField]
		protected float guardianAnimationDuration;

		[SerializeField]
		private Ease guardianAnimationEase;

		[SerializeField]
		private Transform[] extraGuardians;

		[SerializeField]
		private float extraAnimationsStartSecond;

		[SerializeField]
		private Ease extraGuardiansEase;

		protected Sequence guardianSequence;

		private void Start()
		{
			animatedGuardian.SetActive (false);
		}

		public override void StartStep()
		{
			AnimateGuardian ();
		}

		private void AnimateGuardian()
		{
			animatedGuardian.SetActive(true);
			guardianSequence = DOTween.Sequence();
			guardianSequence.Append(animatedGuardian.transform.DOMove(finalGuardian.position, guardianAnimationDuration).SetEase(guardianAnimationEase));

			for (int i = 0; i < extraGuardians.Length; i++)
			{
				guardianSequence.Insert(extraAnimationsStartSecond, extraGuardians[i].
					DOMoveY(finalGuardian.position.y, guardianAnimationDuration - extraAnimationsStartSecond).
					SetEase(extraGuardiansEase));
			}

			guardianSequence.AppendCallback(OnGuardianAnimationComplete);

		}

		private void OnGuardianAnimationComplete()
		{
			EndStep ();
		}
	}
}