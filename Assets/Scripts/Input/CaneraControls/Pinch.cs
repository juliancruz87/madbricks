using UnityEngine;
using ManagerInput.Detail;

namespace ManagerInput.CameraControls
{
	[System.Serializable]
	public class Pinch 
	{
		[SerializeField]
		private float speed = 0.1F; 

		[SerializeField]
		private float minPinchSpeed = 5.0F;

		[SerializeField]
		private float varianceInDistances = 5.0F;
		
		private float touchDelta = 0.0F; 
		private Vector2 prevDist = new Vector2(0,0); 
		private Vector2 curDist = new Vector2(0,0); 
		private float speedTouch0 = 0.0F; 
		private float speedTouch1 = 0.0F;
		private float minPinchToScale = 30F;
		[Range (0,1)]
		private float currentPinchValue = 0.5f;

		private void Update () 
		{
			if (Input.touchCount == 2 && Input.GetTouch(0).phase == TouchPhase.Moved && Input.GetTouch(1).phase == TouchPhase.Moved) 
			{
				curDist = Input.GetTouch(0).position - Input.GetTouch(1).position; //current distance between finger touches
				prevDist = ((Input.GetTouch(0).position - Input.GetTouch(0).deltaPosition) - (Input.GetTouch(1).position - Input.GetTouch(1).deltaPosition)); //difference in previous locations using delta positions
				touchDelta = curDist.magnitude - prevDist.magnitude;
				speedTouch0 = Input.GetTouch(0).deltaPosition.magnitude / Input.GetTouch(0).deltaTime;
				speedTouch1 = Input.GetTouch(1).deltaPosition.magnitude / Input.GetTouch(1).deltaTime;
				
				if ((touchDelta + varianceInDistances <= -minPinchToScale) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
				{
					currentPinchValue = Mathf.Clamp(currentPinchValue - ( 1* speed) , 0 , 1 );
				}
				
				if ((touchDelta +varianceInDistances > minPinchToScale) && (speedTouch0 > minPinchSpeed) && (speedTouch1 > minPinchSpeed))
				{
					currentPinchValue = Mathf.Clamp(currentPinchValue + ( 1* speed) , 0 , 1 );
				}
				
			}       
		}
	}
	
}
