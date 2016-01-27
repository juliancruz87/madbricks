using UnityEngine;
using System.Collections;
using Sound.Detail;

namespace Sound
{
	public class AudioSourceLibAccess : MonoBehaviour 
	{
		[SerializeField]
		private AudioSource instrument;
		[SerializeField]
		private AudioSource snapPlayer;
		[SerializeField]
		private AudioSource launcher;
		[SerializeField]
		private AudioSource collision;
		[SerializeField]
		private AudioSource dialog;
		[SerializeField]
		private AudioSource levelMusic;
		[SerializeField]
		private AudioSource levelMusicComplete;
		[SerializeField]
		private AudioSource explosiveTotem;
		[SerializeField]
		private AudioSource portalSound;
		[SerializeField]
		private AudioSource winLevel;
		[SerializeField]
		private AudioSource loseLevel;
		[SerializeField]
		private ScritableAudioSourceLibLevelSettings settings;

		public AudioSource Instrument 
		{
			get { return instrument; }
		}

		public AudioSource WinLevel 
		{
			get { return winLevel; }
		}

		public AudioSource LoseLevel 
		{
			get { return loseLevel; }
		}

		public AudioSource ExplosiveTotem 
		{
			get { return explosiveTotem; }
		}

		public AudioSource PortalSound 
		{
			get { return portalSound;}
		}

		private void Awake ()
		{
			instrument.clip = settings.Instrument;
			dialog.clip = settings.Dialog;
			levelMusic.clip = settings.MusicLevel;
			levelMusicComplete.clip = settings.MusicLevelComplete;
		}
	}
}