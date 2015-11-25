using UnityEngine;
using ManagerInput.Detail;

namespace ManagerInput.CameraControls
{
	public enum Axis { X=0,Y=1,Z=2,All = 3}
	public class PanRotate : MonoBehaviour 
	{
		[SerializeField]
		private float speed = 1.5F;
		[SerializeField]
		private float minimalDrag = 20F;

		private bool startsPanRotate = false;
		private Transform myTransform;
		private Quaternion initialRot;

		private ITouchInfo TouchInfo
		{
			get { return InputManager.Instance.InputDevice.PrimaryTouch; }
		}

		private void Start()
		{
			myTransform = transform;
			initialRot = myTransform.localRotation;
		}
		
		private void Update()
		{
			if(Input.touchCount >= 4)
				ResetRotation();
			
			if(TouchInfo.IsTouching)
			{
				if(!startsPanRotate)
				{
					startsPanRotate = true;
				}
				else
				{
					InitRotation();
				}
			}
			else
			{
				startsPanRotate = false;
			}
		}
		
		private void InitRotation()
		{
			if(TouchInfo.StartTouchPosition.x < TouchInfo.TouchPosition.x)
			{
				myTransform.Rotate(Vector3.forward , speed * Time.deltaTime );
			}
			else if(TouchInfo.DragDeltaThisFrame.x > minimalDrag)
			{
				myTransform.Rotate(Vector3.forward , -speed * Time.deltaTime );
			}
			
			if(TouchInfo.DragDeltaThisFrame.y < -(minimalDrag))
			{
				myTransform.Rotate(Vector3.right , -speed * Time.deltaTime );
			}
			else if(TouchInfo.DragDeltaThisFrame.y > minimalDrag)
			{
				myTransform.Rotate(Vector3.right , speed * Time.deltaTime );
			}
		}
		
		private void ResetRotation()
		{
			myTransform.localRotation = initialRot;
		}
	}
}