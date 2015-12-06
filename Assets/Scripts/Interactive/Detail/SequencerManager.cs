using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Interactive.Detail
{
	public class SequencerManager : MonoBehaviour 
	{
		public event Action EndIntroduction;

		[SerializeField]
		private int currentStep = 0;

		[SerializeField]
		private List<BeginStepGameBase> steps;

		private void Start ()
		{
			steps.ForEach (c => c.EndStep += OnNextStep);
		}

		private void OnDestroy ()
		{
			steps.ForEach (c => c.EndStep -= OnNextStep);
		} 

		public void Play ()
		{
			StartCurrentStep ();
		}

		private void OnNextStep ()
		{
			currentStep++;
			if (currentStep < steps.Count)
				StartCurrentStep ();
			else 
				EndSequence ();
		}

		private void StartCurrentStep ()
		{
			steps [currentStep].StartStep ();
		}

		private void EndSequence ()
		{
			if (EndIntroduction != null)
				EndIntroduction ();
		}
	}
}