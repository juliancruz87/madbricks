using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sound.Detail
{
	public class ScriptableSoundSettings : ScriptableObject
	{
		[SerializeField]
		private AudioClip defaultSound;
		[SerializeField]
		private List<SoundConfig> sounds = new List<SoundConfig> ();

		public AudioClip GetSound (int id, SoundType type)
		{
			SoundConfig sound = sounds.Find (s => s.Id == id && s.Type == type);
			if (sound == null) 
			{
				Debug.LogWarning ("The sound ["+id+"] and type = "+type+", was not found, please verify the config in settings. You got default sound.");
				return defaultSound;
			}
			else
				return sound.Audio;
		}
	}
}