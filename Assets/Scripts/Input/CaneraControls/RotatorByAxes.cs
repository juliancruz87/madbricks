using UnityEngine;
using ManagerInput.Detail;

namespace ManagerInput.CameraControls
{
	[System.Serializable]
	public class RotatorByAxes : MonoBehaviour
	{
		[SerializeField]
		private float sensitivity = 500.0F;
		[SerializeField]
		private Axis rotateInAxis = Axis.All;
		private bool wasRotated;
		private Transform myTransform;

		private void Start ()
		{
			myTransform = transform;
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
		
		private bool WasRotated 
		{
			get { return wasRotated && !IsMoving; }
		}
		
		private bool IsMoving 
		{
			get{ return Touch.IsDragging; }
		}
		
		private void Stop ()
		{
			wasRotated = false;
		}
		
		public void Rotate ()
		{
			wasRotated = true;
			Vector3 rotateIn = Touch.NormalizedDragDeltaThisFrame * sensitivity;
			SetCurrentRotationByAxis (rotateIn);
		}

		private void Update ()
		{
			if (IsMoving) 
				Rotate ();
			else 
				Stop ();

			myTransform.Rotate (CurrentRotation, Space.World);
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