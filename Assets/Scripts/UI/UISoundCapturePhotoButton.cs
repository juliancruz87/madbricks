using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sound;

public class UISoundCapturePhotoButton : MonoBehaviour , IPointerClickHandler
{
	public void OnPointerClick (PointerEventData eventData)
	{
		SoundManager.Instance.AudioSourceLib.UiButtonCapturePhoto.Play ();
	}
}
