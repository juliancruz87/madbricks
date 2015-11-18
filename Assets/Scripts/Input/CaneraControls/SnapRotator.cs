using System;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using ManagerInput;

namespace ManagerInput.CameraControls
{
	[System.Serializable]
	public class SnapRotator : MonoBehaviour
	{
		private const int MAX_DREGREES = 360;
		[SerializeField]
		private float currentSpeed = 10.0F;
		[SerializeField]
		private List<float> snapRotations = new List<float> ();
		private float cacheDegrees = 0F;
		private Transform myTransform;

		private ITouchInfo Touch
		{
			get { return InputManager.Instance.InputDevice.PrimaryTouch; }
		}

		public Quaternion CurrentRotation
		{
			get;
			private set;
		}

		public int IndexRotaion 
		{
			get;
			private set;
		}

		private void Start ()
		{
			myTransform = transform;
		}

		private void Update ()
		{
			if(!Touch.IsTouching)
			{
				UpdateRotation (myTransform.localRotation, myTransform.localEulerAngles.y);
				myTransform.localRotation = CurrentRotation;
			}
		}

		public void UpdateRotation (Quaternion currentRotation, float currentDegrees)
		{
			float closerDegress = GetCloserDegrees (currentDegrees);
			Vector3 newAngles = new Vector3 (currentRotation.eulerAngles.x,closerDegress,currentRotation.eulerAngles.z);
			Quaternion newRot = Quaternion.Euler (newAngles);
			IndexRotaion = GetCurrentIndexRotation (closerDegress);
			CurrentRotation = Quaternion.Lerp (currentRotation,newRot,Time.deltaTime * currentSpeed);
		}

		private int GetCurrentIndexRotation (float closerDegress)
		{
			if(cacheDegrees != closerDegress)
			{
				cacheDegrees = closerDegress;
				int currentIndex = snapRotations.IndexOf (closerDegress);
				return currentIndex < 0 ? 0 : currentIndex; 
			}
			return IndexRotaion;
		}

		private float GetCloserDegrees (float currentDegrees)
		{
			float closerDegress = NumberHelper.GetCloserFloatInList (MAX_DREGREES, currentDegrees , snapRotations);
			return closerDegress == 360 ? 0 : closerDegress; 
		}
	}
}