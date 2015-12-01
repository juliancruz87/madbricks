using UnityEngine;
using System.Collections;
using ManagerInput;
using System;

namespace InteractiveObjects.Detail
{
	public class DragableItem : MonoBehaviour 
	{ 
		public event Action Release;

		private Collider myCollider;
		private CharacterController myRigidbody;
		private Vector3 nextPosition;
		private Transform myTransform;
		private float yPos;
		private bool wasTouched = false;

		private Camera MainCamera
		{
			get{ return Camera.main;}
		}

		private ITouchInfo PrimaryTouch
		{
			get { return InputManager.Instance.InputDevice.PrimaryTouch; }
		}

		private void Start ()
		{
			myTransform = GetComponent<Transform> ();
			myCollider = GetComponent<Collider> ();
			yPos = myTransform.position.y;
		}

		private void Update()
	    {
			if (TouchChecker.IsTouchingFromCollider (MainCamera, myCollider, false) && TouchChecker.NoHasColliderTouched ())
			{
				TouchChecker.SetLastColliderTouched (myCollider);
				wasTouched = true;
			}

			if (wasTouched && PrimaryTouch.IsTouching)
				TryDrag ();

			if (PrimaryTouch.ReleasedTouchThisFrame && wasTouched) 
			{
				TouchChecker.ReleaseLastColliderTouched ();
				wasTouched = false;
				if(Release != null)
					Release ();
			}
		}

		public virtual void TryDrag()
	    {
			CalculateNextPosition();
			myTransform.position = nextPosition;
	    }

		public virtual void CalculateNextPosition()
	    {
	        Vector3 touchPosition = PrimaryTouch.TouchPosition;
			float distance = MainCamera.WorldToScreenPoint(myTransform.position).z;
			nextPosition = MainCamera.ScreenToWorldPoint(new Vector3(touchPosition.x, touchPosition.y, distance ));
			nextPosition = new Vector3 (nextPosition.x, yPos, nextPosition.z);
	    }
	}
}