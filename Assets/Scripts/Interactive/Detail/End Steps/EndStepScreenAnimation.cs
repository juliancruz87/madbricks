using System;
using UnityEngine;
using System.Collections;
using Sound;
using DG.Tweening;
using UnityEngine.UI;

namespace Interactive.Detail
{
	public class EndStepScreenAnimation : BeginStepGameBase
	{
		[SerializeField]
		private CanvasGroup winScreen;

		[SerializeField]
		private float screenAnimationDuration;

		[SerializeField]
		private GameObject[] objectsToActivate;

		private void Start()
		{
			for (int i = 0; i < objectsToActivate.Length; i++) {
				objectsToActivate [i].SetActive (false);
			}
		}

		public override void StartStep()
		{
			for (int i = 0; i < objectsToActivate.Length; i++) {
				objectsToActivate [i].SetActive (true);
			}

			winScreen.blocksRaycasts = true;
			winScreen.DOFade(1, screenAnimationDuration).OnComplete(OnScreenAnimationComplete); 
		}

		private void OnScreenAnimationComplete()
		{
			EndStep ();
		}
	}
}