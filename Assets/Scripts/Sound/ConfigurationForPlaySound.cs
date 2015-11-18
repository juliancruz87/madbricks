using UnityEngine;
using System.Collections;

namespace Sound
{
	[System.Serializable]
	public class ConfigurationForPlaySound
	{
		[SerializeField]
		private SoundType soundType;
		[SerializeField]
		private int id;

		public SoundType Type 
		{
			get { return soundType;}
		}

		public int Id 
		{
			get { return id; }
		}
	}
	
}