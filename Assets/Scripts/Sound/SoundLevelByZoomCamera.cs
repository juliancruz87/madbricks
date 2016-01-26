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
		[SerializeField]
		private float maxCapped = 0F;
		[SerializeField]
		private float minCapped = 0F;
		[SerializeField]
		private string paramToChange = "";
		[SerializeField]
		private bool reverse = false;

		private float lastVolume = 0;
		private ZoneCamera cameraZoom;

		private void Awake ()
		{
			cameraZoom = FindObjectOfType<ZoneCamera> ();
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
				float currentVolume = GetCurrentVolume ();
				currentVolume = Mathf.Clamp (currentVolume, minCapped, maxCapped);
				mixer.SetFloat (paramToChange, currentVolume);
			}
		}

		private float GetCurrentVolume ()
		{
			if(reverse)
				return Mathf.Lerp (maxDecibels, minDecibels, cameraZoom.CurrentZoomValue);
			return Mathf.Lerp (minDecibels, maxDecibels, cameraZoom.CurrentZoomValue);
		}
	}
}