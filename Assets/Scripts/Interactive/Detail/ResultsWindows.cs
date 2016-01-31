using System;
using UnityEngine;
using System.Collections;
using Sound;

namespace Interactive.Detail
{
	public class ResultsWindows : BeginStepGameBase
	{
		private IGameManagerForUI GameManagerForUI
		{
			get { return GameManager.Instance;}
		}

		[SerializeField]
		private GameObject winResults;

		[SerializeField]
		private GameObject loseResults;

		public override void StartStep ()
		{
            ShowResults ();

		}

		private void ShowResults ()
		{
			if (GameManagerForUI.Result == GameResults.Win) {
				//winResults.SetActive(true);
				LevelLoaderController.LevelLoader.Instance.LoadNextLevel ();
			}
			else {
				// loseResults.SetActive(true);
				Application.LoadLevel (Application.loadedLevelName);
			}
			EndStep ();
		}
	}
	
}