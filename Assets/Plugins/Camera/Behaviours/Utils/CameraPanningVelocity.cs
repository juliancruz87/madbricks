using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace CameraTools.Detail
{
	[System.Serializable]
	public class CameraPanningVelocity
	{
		// How many velocity samples (from previous frames) are taken to calculate the current velocity
		[SerializeField]
		private int velocitySamples = 5;
		
		[SerializeField]
		private float inertiaEffectDeacceleration = 100.0f;	
		
		[SerializeField]
		private float inertiaEffectMaxSpeed = 30.0f;

		private Queue<Vector3> lastCamPanVelocities = new Queue<Vector3> ();	
		
		public Vector3 CurrentVelocity 
		{
			get;
			private set;
		}
		
		public void UpdateWhileDragging (Vector3 dragDeltaThisFrameProjectedOnFloor)
		{		
			UpdatePanningVelocityQueue(dragDeltaThisFrameProjectedOnFloor);
			CalcCurrentVelUsingSamples();
		}
		
		public void UpdateWhileNotDragging ()
		{
			lastCamPanVelocities.Clear ();
			CalcCurrentVelUsingDeacceleration();
		}

		private void UpdatePanningVelocityQueue(Vector3 dragDeltaThisFrameProjectedOnFloor)
		{
			Vector3 velThisFrame = -dragDeltaThisFrameProjectedOnFloor / Time.deltaTime;		
			lastCamPanVelocities.Enqueue (velThisFrame);
			if (lastCamPanVelocities.Count > velocitySamples)
				lastCamPanVelocities.Dequeue ();
		}

		private void CalcCurrentVelUsingSamples()
		{
			CurrentVelocity = new Vector3 ();
			foreach (Vector3 v in lastCamPanVelocities)
				CurrentVelocity += v;
			CurrentVelocity /= (float)lastCamPanVelocities.Count;
		}
		
		private void CalcCurrentVelUsingDeacceleration()
		{
			float oldSpeed = CurrentVelocity.magnitude;
			float deacceleration = inertiaEffectDeacceleration * Time.deltaTime;

			if (deacceleration < oldSpeed)
			{
				float newSpeed = Mathf.Clamp(oldSpeed - deacceleration, 0.0f, inertiaEffectMaxSpeed);
				Vector3 normalizedVel = CurrentVelocity / oldSpeed;
				CurrentVelocity = normalizedVel * newSpeed;
			}
			else
			{
				CurrentVelocity = Vector3.zero;
			}
		}
	}
}
