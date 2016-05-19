using UnityEngine;
using ManagerInput;
using Interactive;
using DG.Tweening;
using CameraTools;

public class Handler : MonoBehaviour 
{
	[SerializeField]
	private Collider myCollider;

	[SerializeField]
	private Vector3 strenght = new Vector3(0.1f, 0.1f, 0.1f);

	[SerializeField]
	private int vibrato = 30;

	[SerializeField]
	private float duration = 0.5f;

	private Camera mainCamera;
	private CameraManager cameraManager; 

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
		
		if (TouchChecker.WasTappingFromCollider(Camera.main, myCollider, true))
        {
			if (GameManager.Instance.IsEveryTotemOnLauncher) {
				animator.SetTrigger ("Start");
				GameManagerForStates.Play ();
			} 
			else 
			{
				mainCamera = Camera.main;
				cameraManager = mainCamera.gameObject.GetComponent<CameraManager>();
				cameraManager.enabled = false;
				mainCamera.DOShakePosition (duration, strenght, vibrato).OnComplete(OnShakeComplete);
				animator.SetTrigger ("Break");
			}
       }       
	}


	private void OnShakeComplete()
	{
		cameraManager.enabled = true;
	}


	public void StopAnimation()
	{
		animator.Stop ();
	}
}