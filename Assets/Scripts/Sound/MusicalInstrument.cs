using UnityEngine;
using System.Collections;
using Sound;
using ManagerInput;

public class MusicalInstrument : MonoBehaviour
{
    private AudioSource audioSource;
    private Collider myCollider;
    private Animator myAnimator;

	private void Start () 
	{
        myCollider = GetComponent<Collider>();
        myAnimator = GetComponent<Animator>();
		audioSource = SoundManager.Instance.AudioSourceLib.Instrument;
	}

	private void Update () 
	{
	    CheckInput();
	}

    private void CheckInput() {

        if (TouchChecker.WasTappingFromCollider(Camera.main, myCollider) && IsAllowedToPlay()) 
        { 
            audioSource.Play();
            myAnimator.SetTrigger("PlayInstrument");
        }
    }
    
    private bool IsAllowedToPlay() {
        return !audioSource.isPlaying && !myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Take 001");
    }
}
