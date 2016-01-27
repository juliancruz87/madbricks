using UnityEngine;
using System.Collections;
using ManagerInput;
using Sound.Detail;
using Interactive;

namespace Sound
{
	[RequireComponent (typeof (AudioSource))]
	public class SoundManager : MonoBehaviour 
	{
		[SerializeField]
		private ScriptableSoundSettings soundSettings;
		private AudioSource audioSource;
		private AudioSourceLibAccess audioSourceLib;

		public AudioSourceLibAccess AudioSourceLib 
		{
			get
			{
				if(audioSourceLib == null)
					audioSourceLib = FindObjectOfType<AudioSourceLibAccess> ();
				return audioSourceLib;
			}
		}

		public static SoundManager Instance 
		{
			get;
			private set;
		}
		
		private void Awake ()
		{
			if (Instance == null) 
				Instance = this;
			else
				Destroy (gameObject);
		}

		private void Start ()
		{
			audioSource = GetComponent<AudioSource> ();
			if (audioSource == null)
				gameObject.AddComponent <AudioSource>();
		}

		public void Play (ConfigurationForPlaySound soundType)
		{
			if(soundType.Type != SoundType.None)
			{
				AudioClip sound = soundSettings.GetSound (soundType.Id, soundType.Type);
				PlayOneShot (sound);
			}
		}

		public void Play (AudioClip sound)
		{
			PlayOneShot (sound);
		}

		private void PlayOneShot (AudioClip sound)
		{
			audioSource.PlayOneShot (sound);
		}

		public void PlayEndSound (GameResults results)
		{
			if (results == GameResults.Win)
				audioSourceLib.WinLevel.Play ();
			else
				audioSourceLib.LoseLevel.Play ();
		}
	}
}