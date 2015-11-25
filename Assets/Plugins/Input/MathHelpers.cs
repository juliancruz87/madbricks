using UnityEngine;
using System.Collections;

namespace ManagerInput.Detail
{
	public static class MathHelpers 
	{
		public static float GetEasedOutPct(float originalPct, float easeOutExponent)
		{
			return 1.0f - Mathf.Pow((1.0f - originalPct), easeOutExponent);		
		}

		public static float GetAngleBetweenVectorsXYPlane (Vector3 fromDir, Vector3 toDir)
		{
			float angle = Vector3.Angle (fromDir, toDir);
			Vector3 cross = Vector3.Cross (fromDir, toDir);
			
			if (cross.z > 0) 
				angle = -angle;
			
			return angle;
		}
		
		public static float GetAngleBetweenVectorsXZPlane(Vector3 fromDir, Vector3 toDir)
		{
			float angle = Vector3.Angle (fromDir, toDir);
			Vector3 cross = Vector3.Cross (fromDir, toDir);
			
			if (cross.y > 0)
				angle = -angle;
			
			return angle;
		}
	}
}
