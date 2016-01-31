using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Sound;

public class UISoundButton : MonoBehaviour , IPointerClickHandler
{
	public void OnPointerClick (PointerEventData eventData)
	{
		SoundManager.Instance.AudioSourceLib.UiButtonSound.Play ();
	}
}