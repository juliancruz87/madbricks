using System;
using UnityEngine;
using System.Collections;
using Sound;
using Zenject;

namespace Interactive.Detail
{
	public class ResultsWindows : BeginStepGameBase
	{
		[Inject]
		private IGameManagerForUI gameManagerForUI;

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
			if (gameManagerForUI.Result == GameResults.Win) {
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