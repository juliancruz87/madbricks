using UnityEngine;
using System.Collections;
using ManagerInput;
using Interactive;

public class Handler : MonoBehaviour 
{
	[SerializeField]
	private Collider myCollider;

    private Animator animator;

	private IGameManagerForStates GameManagerForStates
	{
		get { return GameManager.Instance;}
	}

    private void Awake ()
    {
        animator = GetComponent<Animator>();
    }

	private void Update () 
	{
		if ( GameManagerForStates.CurrentState == GameStates.Planning && 
            TouchChecker.WasTappingFromCollider(Camera.main, myCollider))
            {
                animator.SetTrigger ("Start");
                
                //GameManagerForStates.Play();
            }
            
	}
}