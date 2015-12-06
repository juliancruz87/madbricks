using UnityEngine;
using System.Collections;

namespace CameraTools.Detail
{
	public class AABRectForOnScreenArea
	{
		public float minX;
		public float maxX;
		public float minZ;
		public float maxZ;
		
		public void Calculate()
		{
			// Get current extreme points (points in floor in screen corners)
			Vector3 currentBottomLeft = CameraManager.GetPointInFloorFromScreenPoint(new Vector3(0, 0, 0));
			Vector3 currentBottomRight = CameraManager.GetPointInFloorFromScreenPoint(new Vector3(Screen.width, 0, 0));
			Vector3 currentTopLeft = CameraManager.GetPointInFloorFromScreenPoint(new Vector3(0, Screen.height, 0));
			Vector3 currentTopRight = CameraManager.GetPointInFloorFromScreenPoint(new Vector3(Screen.width, Screen.height, 0));
			
			// Get the AAB rect for the extreme points
			minX = Mathf.Min(currentBottomLeft.x, currentBottomRight.x, currentTopLeft.x, currentTopRight.x);
			maxX = Mathf.Max(currentBottomLeft.x, currentBottomRight.x, currentTopLeft.x, currentTopRight.x);
			minZ = Mathf.Min(currentBottomLeft.z, currentBottomRight.z, currentTopLeft.z, currentTopRight.z);
			maxZ = Mathf.Max(currentBottomLeft.z, currentBottomRight.z, currentTopLeft.z, currentTopRight.z);
		}
		
		public void AdjustPanningDeltaMoveToFollowLimits(ref Vector3 deltaMove, Vector3 limitMinPoint, Vector3 limitMaxPoint)
		{
			Calculate();
			
			// Check map x+ border
			if (deltaMove.x > 0)
			{
				float limit = limitMaxPoint.x;
				if (maxX + deltaMove.x > limit)
					deltaMove.x = limit - maxX;
			}		
			// Check map x- border
			else
			{
				float limit = limitMinPoint.x;
				if (minX + deltaMove.x < limit)
					deltaMove.x = limit - minX;
			}
			
			// Check map z+ border
			if (deltaMove.z > 0)
			{
				float limit = limitMaxPoint.z;
				if (maxZ + deltaMove.z > limit)
					deltaMove.z = limit - maxZ;
			}		
			// Check map z- border
			else
			{
				float limit = limitMinPoint.z;
				if (minZ + deltaMove.z < limit)
					deltaMove.z = limit - minZ;
			}
		}
	}
}