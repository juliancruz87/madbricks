using System;
using UnityEngine;
using System.Collections;
using Sound;

namespace Interactive.Detail
{
	public class LoadLevelStep : BeginStepGameBase
	{
		private IGameManagerForUI GameManagerForUI
		{
			get { return GameManager.Instance;}
		}
			
		public override void StartStep ()
		{
            LoadLevel ();

		}

		private void LoadLevel ()
		{
			if (GameManagerForUI.Result == GameResults.Win)
				LevelLoaderController.LevelLoader.Instance.LoadNextLevel ();
			else 
				Application.LoadLevel (Application.loadedLevelName);
			
			EndStep ();
		}
	}
	
}