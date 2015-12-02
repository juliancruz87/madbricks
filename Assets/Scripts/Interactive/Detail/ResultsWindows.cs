using System;
using UnityEngine;
using System.Collections;
using Sound;

namespace Interactive.Detail
{
	public class ResultsWindows : BeginStepGameBase
	{
		[SerializeField]
		private GameObject winResults;

		[SerializeField]
		private GameObject loseResults;

		public override void StartStep ()
		{
			if(GameManagerAccess.GameManagerState.Result == GameResults.Win)
				winResults.SetActive (true);
			else
				loseResults.SetActive (true);

			EndStep ();
		}
	}
	
}