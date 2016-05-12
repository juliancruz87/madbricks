using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

namespace Interactive.Detail
{
	public class SequencerManager : MonoBehaviour 
	{
		public event Action SequenceEnded;

		[SerializeField]
		private int currentStep = 0;

		[SerializeField]
		private List<BeginStepGameBase> steps;

		private void Awake ()
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
			if (steps.Count > currentStep)
				StartCurrentStep ();
			else 
				OnSequenceEnded ();
		}

		private void StartCurrentStep ()
		{
			if(steps.Count > currentStep)
				steps [currentStep].StartStep ();
		}

		private void OnSequenceEnded ()
		{
			if (SequenceEnded != null)
				SequenceEnded ();
		}
	}
}