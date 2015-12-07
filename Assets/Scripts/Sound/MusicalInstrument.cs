using UnityEngine;
using System.Collections;

public class MusicalInstrument : MonoBehaviour
{
    [SerializeField] 
    private AudioSource audioSource;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
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
