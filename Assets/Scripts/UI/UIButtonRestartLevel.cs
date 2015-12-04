using UnityEngine;
using System.Collections;
using LevelLoaderController;

namespace UI
{
	public class UIButtonRestartLevel : MonoBehaviour 
	{
		public void OnClickEvent ()
		{
			LevelLoader.Instance.LoadScene (Application.loadedLevelName);
		}
	}
}