using UnityEngine;
using System.Collections;
using ManagerInput;

namespace InteractiveObjects.Detail
{
	public class DragableItem : MonoBehaviour 
	{ 
		private CharacterController myRigidbody;
		protected Vector3 nextPosition;
	    protected bool isInRelease;

		public virtual IInputDeviceTouchData InputDevice
	    {
	        get { return InputManager.Instance.InputDevice; }
	    }

		private void Start ()
		{
			myRigidbody = GetComponent<CharacterController> ();
		}

		public virtual ITouchInfo PrimaryTouch
	    {
	        get { return InputManager.Instance.InputDevice.PrimaryTouch; }
	    }

	    public virtual void Initialize(Vector3 initialPosition)
	    {
	        isInRelease = false;
	        transform.position = initialPosition;
	        TryDrag();
	    }

		public virtual void Update()
	    {
	        if (InputDevice.TouchCount == 1)
	            CheckInputState();
	        else if (!isInRelease)
	            OnRelease();
	    }

		public virtual void OnRelease()
	    {
	        isInRelease = true;
	    }

		public virtual void CheckInputState()
	    {
	        if (PrimaryTouch.IsDragging)
	            TryDrag();
	    }

		public virtual void TryDrag()
	    {
	        CalculateNextPosition();
			myRigidbody.SimpleMove(nextPosition);
	    }

		public virtual void CalculateNextPosition()
	    {
	        float distance = Camera.main.WorldToScreenPoint(transform.position).z;
			float distanceY = Camera.main.WorldToScreenPoint(transform.position).y;
	        Vector3 touchPosition = PrimaryTouch.TouchPosition;
			nextPosition = Camera.main.ScreenToWorldPoint(new Vector3(touchPosition.x, distanceY, distance ));
	    }
	}
}
