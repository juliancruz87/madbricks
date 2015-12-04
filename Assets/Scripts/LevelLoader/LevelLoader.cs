using UnityEngine;
using System.Collections;
using LevelLoaderController.Detail;

namespace LevelLoaderController
{
	public class LevelLoader : MonoBehaviour 
	{
		[SerializeField]
		private LevelLoaderSettings settings;

		private string pendingScene = string.Empty;
		
		public static LevelLoader Instance 
		{
			get;
			private set;
		}

		private void Awake ()
		{
			if (Instance == null) 
			{
				DontDestroyOnLoad (gameObject);
				Instance = this;
			}
		}

		public void LoadScene(string levelName)
		{
			pendingScene = levelName;
			Application.LoadLevel (settings.LevelLoader);
		}

		public void LoadPendingScene()
		{
			StartCoroutine (LoadLevelAsync());
		}
		
		private IEnumerator LoadLevelAsync()
		{	
			System.GC.Collect ();
			System.GC.WaitForPendingFinalizers ();
			yield return Resources.UnloadUnusedAssets();
			yield return Application.LoadLevelAsync (pendingScene);
		}
	}	
}