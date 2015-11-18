using UnityEngine;
using ManagerInput.Detail;

namespace ManagerInput.CameraControls
{
	public class RotateGesture : MonoBehaviour
	{	
		public bool canRotate;
		public float rotGestureWidth;
		public float rotAngleMinimium;
		public float speed = 1F;
		public Vector2 fingersDistance;
		
		private Vector3 rotateAxis;
		private Transform myTransform;
		
		private void Start()
		{
			myTransform = transform;
		}
		
		public void Update () 
		{
			if(Input.touchCount == 2)
			{
				GORotate();
			}
			else
			{
				canRotate = false;
			}
		}
		
		private void GORotate()
		{
			if(!canRotate)
			{
				fingersDistance = Input.GetTouch(1).position - Input.GetTouch(0).position;
				canRotate = fingersDistance.sqrMagnitude > (rotGestureWidth * rotGestureWidth);
				return;
			}
			
			Vector2 currentDistance = Input.GetTouch(1).position - Input.GetTouch(0).position;
			float angleOffset = Vector2.Angle( fingersDistance , currentDistance );
			Vector3 LR = Vector3.Cross( fingersDistance , currentDistance );
			
			if( angleOffset > rotAngleMinimium )
			{
				if( LR.z > 0 )
				{
					myTransform.Rotate(Vector3.up,-speed*Time.deltaTime);
				}
				else if( LR.z < 0 )
				{
					myTransform.Rotate(Vector3.up,speed*Time.deltaTime);
				}
			}
			
		}
		
	}
	
}
