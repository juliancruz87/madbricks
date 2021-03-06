using UnityEngine;
using ManagerInput.Detail;
using Interactive;
using Sound;
using UnityEngine.EventSystems;

namespace ManagerInput.CameraControls
{
	[System.Serializable]
	public class RotatorByAxes : MonoBehaviour
	{
		[SerializeField]
		private Collider myCollider;

		[SerializeField]
		private float sensitivity = 500.0F;
	 
		[SerializeField]
		private Axis rotateInAxis = Axis.All;

		private IGameManagerForStates GameManagerForStates
		{
			get { return GameManager.Instance;}
		}

		private Transform myTransform;
		private ConditionalDrag conditional = new ConditionalDrag ();

		private bool CanDrag
		{
			get{ return conditional.Check ();}
		}
		
		private ITouchInfo Touch
		{
			get { return InputManager.Instance.InputDevice.PrimaryTouch; }
		}
		
		private Vector3 CurrentRotation
		{
			get; 
			set;
		}

        public Collider Collider
        {
            get { return myCollider; }
        }

        private void Start ()
		{
			
			conditional.GameManager = GameManagerForStates;
			myTransform = transform;
		}

		private void Rotate ()
		{
			Vector3 rotateIn = Touch.NormalizedDragDeltaThisFrame * sensitivity;
			SetCurrentRotationByAxis (rotateIn);
		}

		private void Update ()
		{
			if (CanDrag && Touch.IsDragging && TouchChecker.IsTouchingFromCollider (Camera.main, myCollider, true, true)) 
			{
				if(!SoundManager.Instance.AudioSourceLib.RotationMusicBox.isPlaying)
					SoundManager.Instance.AudioSourceLib.RotationMusicBox.Play ();

				Rotate ();
				myTransform.Rotate (CurrentRotation, Space.World);
			}
		}

		private void LateUpdate ()
		{
			if (Touch.ReleasedTouchThisFrame && SoundManager.Instance.AudioSourceLib.RotationMusicBox.isPlaying)
				SoundManager.Instance.AudioSourceLib.RotationMusicBox.Stop ();
		}

		private void SetCurrentRotationByAxis (Vector3 rotateIn)
		{
			switch (rotateInAxis) 
			{
			default:
				CurrentRotation = new Vector3 (rotateIn.y, -rotateIn.x, rotateIn.z);
				break;
			case Axis.X:
				CurrentRotation = new Vector3 (rotateIn.y, 0, 0);
				break;
			case Axis.Y:
				CurrentRotation = new Vector3 (0, -rotateIn.x, 0);
				break;
			case Axis.Z:
				CurrentRotation = new Vector3 (0, 0, rotateIn.z);
				break;
			}
		}
	}
}