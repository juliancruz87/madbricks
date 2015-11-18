using UnityEngine;
using System.Collections;

namespace Graphics.Detail
{
	public class Billboard : MonoBehaviour
	{
		private enum Axis {up, down, left, right, forward, back};

		[SerializeField]
		public bool reverseFace = false; 

		[SerializeField]
		private Axis axis = Axis.up; 

		[SerializeField]
		private Transform pointToView;

		private Transform myTransform;
		
		private void  Awake ()
		{
			if (!pointToView)
				pointToView = Camera.main.transform; 
			myTransform = transform;
		}
		
		private void  Update ()
		{
			RotateDirectionCamera ();
		}

		private void RotateDirectionCamera ()
		{
			Vector3 targetPos = myTransform.position + pointToView.rotation * (reverseFace ? Vector3.forward : Vector3.back);
			Vector3 targetOrientation = pointToView.rotation * GetAxisForDirection (axis);
			myTransform.LookAt (targetPos, targetOrientation);
		}

		private Vector3 GetAxisForDirection (Axis refAxis)
		{
			switch (refAxis)
			{
			case Axis.down:
				return Vector3.down; 
			case Axis.forward:
				return Vector3.forward; 
			case Axis.back:
				return Vector3.back; 
			case Axis.left:
				return Vector3.left; 
			case Axis.right:
				return Vector3.right; 
			default:
				return Vector3.up;
			}	
		}
	}
}