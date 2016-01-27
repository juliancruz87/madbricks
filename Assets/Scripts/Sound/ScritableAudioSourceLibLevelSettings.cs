using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Sound.Detail
{
	public class ScritableAudioSourceLibLevelSettings : ScriptableObject
	{
		[SerializeField]
		private AudioClip instrument;
		[SerializeField]
		private AudioClip musicLevel;
		[SerializeField]
		private AudioClip musicLevelComplete;
		[SerializeField]
		private AudioClip dialog;

		public AudioClip Instrument {
			get {
				return instrument;
			}
		}

		public AudioClip MusicLevel {
			get {
				return musicLevel;
			}
		}

		public AudioClip MusicLevelComplete {
			get {
				return musicLevelComplete;
			}
		}

		public AudioClip Dialog {
			get {
				return dialog;
			}
		}
	}
	
}