using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace LevelLoaderController.Detail
{
	public class LevelLoaderSettings : ScriptableObject
	{
		[SerializeField]
		private string levelLoader;

		public string LevelLoader 
		{
			get { return levelLoader; }
		}
	}
}