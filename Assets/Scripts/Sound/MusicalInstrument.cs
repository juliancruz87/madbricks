using UnityEngine;
using System.Collections;
using Sound;

public class MusicalInstrument : MonoBehaviour
{
    private AudioSource audioSource;
	
	private void Start () 
	{
		audioSource = SoundManager.Instance.AudioSourceLib.Instrument;
	}

	private void Update () 
	{
	    CheckInput();
	}

    private void CheckInput() {
        if (Input.GetMouseButtonDown(0)) {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit[] raycastHits = Physics.RaycastAll(ray);

            if (ThisGameObjectWasHitted(raycastHits) &&
                IsAllowedToPlay()) 
                audioSource.Play();
        }
    }

    private bool ThisGameObjectWasHitted(RaycastHit[] raycastHits) {
        GameObject firstGameObject = null;
        float nearestDistance = float.MaxValue;
        foreach (RaycastHit hitinfo in raycastHits) {
            float distance = Vector3.Distance(Camera.main.transform.position, hitinfo.collider.transform.position);
            if (distance < nearestDistance) {
                    firstGameObject = hitinfo.collider.gameObject;
                    nearestDistance = distance;
            }
        }

        return firstGameObject == gameObject;
    }
    
    private bool IsAllowedToPlay() {
        //TODO: Complete
        return !audioSource.isPlaying;
    }
}
