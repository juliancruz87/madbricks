using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Interactive.Detail
{
	public class BeginGameManager : MonoBehaviour 
	{
		public event Action EndIntroduction;

		[SerializeField]
		private int currentStep = 0;
		[SerializeField]
		private List<BeginStepGameBase> steps;

		private IEnumerator Start ()
		{
			steps.ForEach (c => c.EndStep += OnNextStep);
			yield return new WaitForSeconds (1F);
			Init ();
		}

		private void OnDestroy ()
		{
			steps.ForEach (c => c.EndStep -= OnNextStep);
		} 

		public void Init ()
		{
			StartCurrentStep ();
		}

		private void OnNextStep ()
		{
			currentStep++;
			if (currentStep < steps.Count)
				StartCurrentStep ();
			else if (EndIntroduction != null)
				EndIntroduction ();
		}

		private void StartCurrentStep ()
		{
			steps [currentStep].StartStep ();
		}
	}
}