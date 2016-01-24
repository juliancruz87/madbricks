using UnityEngine;
using System.Collections;
using Interactive.Detail;
using UnityEngine.Audio;

namespace Sound
{
	public class SoundLevelByZoomCamera : MonoBehaviour
	{
		[SerializeField]
		private AudioMixer mixer;
		[SerializeField]
		private float minDecibels = -80F;
		[SerializeField]
		private float maxDecibels = 1F;

		private float lastVolume = 0;
		private ZoneCamera cameraZoom;

		private void Awake ()
		{
			cameraZoom = gameObject.GetComponent<ZoneCamera> ();
			UpdateVolumeByZoom ();
		}

		private void Update ()
		{
			if(cameraZoom == null)
				return;

			UpdateVolumeByZoom ();
		}

		private void UpdateVolumeByZoom ()
		{
			if (cameraZoom.CurrentZoomValue != lastVolume) 
			{
				lastVolume = cameraZoom.CurrentZoomValue;
				float currentVolume = Mathf.Lerp (minDecibels, maxDecibels, cameraZoom.CurrentZoomValue);
				float currentVolume2 = Mathf.Lerp (maxDecibels, minDecibels, cameraZoom.CurrentZoomValue);
				mixer.SetFloat ("MusicVolume", currentVolume);
				mixer.SetFloat ("ThemeVolume", currentVolume2);
			}
		}
	}
}