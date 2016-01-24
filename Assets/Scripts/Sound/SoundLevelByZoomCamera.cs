using UnityEngine;
using System.Collections;
using Interactive.Detail;
using UnityEngine.Audio;

namespace Sound
{
	public class SoundLevelByZoomCamera : MonoBehaviour
	{
		[SerializeField]
		private AudioSource mainSound;
		[SerializeField]
		private AudioSource hiddenSound;

		private ZoneCamera cameraZoom;

		private void Awake ()
		{
			cameraZoom = gameObject.GetComponent<ZoneCamera> ();
		}

		private void Update ()
		{
			if(cameraZoom == null)
				return;

			if(hiddenSound.volume != cameraZoom.CurrentZoomValue)
			{
				mainSound.volume = 1;
				hiddenSound.volume = cameraZoom.CurrentZoomValue;
				mainSound.volume -= hiddenSound.volume;
			}
		}
	}
}