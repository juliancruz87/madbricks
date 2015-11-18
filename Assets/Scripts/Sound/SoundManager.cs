using UnityEngine;
using System.Collections;
using ManagerInput;
using Sound.Detail;

namespace Sound
{
	[RequireComponent (typeof (AudioSource))]
	public class SoundManager : MonoBehaviour 
	{
		[SerializeField]
		private ScriptableSoundSettings soundSettings;
		private AudioSource audioSource;

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
				audioSource.PlayOneShot (sound);
			}
		}
	}
}