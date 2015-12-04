using UnityEngine;
using System.Collections;

namespace LevelLoaderController.Detail
{
	public class LoadingScene : MonoBehaviour 
	{
		private void Start ()
		{
			LevelLoader.Instance.LoadPendingScene ();
		}
	}
		
}