using UnityEngine;
using System.Collections;

namespace Sound.Detail
{
	[System.Serializable] 
	public class SoundConfig : ConfigurationForPlaySound
	{
		[SerializeField]
		private AudioClip audio;
	
		public AudioClip Audio 
		{
			get { return audio; }
		}
	}
	
}