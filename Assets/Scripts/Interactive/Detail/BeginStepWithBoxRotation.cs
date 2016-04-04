using System;
using UnityEngine;
using System.Collections;
using DG.Tweening;


namespace Interactive.Detail
{
	public class BeginStepWithBoxRotation: BeginStepGameBase
	{
		[SerializeField]
		private Transform box;
        [SerializeField]
        private float rotationTime = 3;

		public override void StartStep ()
		{
            Vector3 endPosition = new Vector3(0, 180, 0);
            box.DORotate(endPosition, rotationTime);
            StartCoroutine(OnEndStepWithTimer(rotationTime));
        }
	}	
}