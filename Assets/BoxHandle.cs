using UnityEngine;
using System.Collections;

public class BoxHandle : MonoBehaviour {


	void Start () {

        Animator animator = GetComponent<Animator>();
        animator.SetTrigger("Rotation");
    }
	

}
