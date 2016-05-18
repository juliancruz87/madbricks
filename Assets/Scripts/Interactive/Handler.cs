using UnityEngine;
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
		animator = GetComponentInChildren<Animator>();
    }

	private void Update () 
	{
		
		if (TouchChecker.WasTappingFromCollider(Camera.main, myCollider, true) && GameManager.Instance.IsEveryTotemOnLauncher)
        {
			if (GameManager.Instance.IsEveryTotemOnLauncher) {
				animator.SetTrigger ("Start");
				GameManagerForStates.Play ();
			} 
			else 
			{
				animator.SetTrigger ("Break");
			}
       }

            
	}

	public void StopAnimation()
	{
		animator.Stop ();
	}
}