using UnityEngine;
using System.Collections;

namespace CameraTools.Detail
{
	[System.Serializable]
	public class CameraSpringEffect
	{
		[SerializeField]
		private float springEffectAreaSize = 2.0f;
		
		[SerializeField]
		private float springEffectSpeed = 5.0f;
		
		public bool Calculate(Vector3 minLimit, Vector3 maxLimit, out Vector3 delta)
		{
			AABRectForOnScreenArea rect = new AABRectForOnScreenArea();
			rect.Calculate();
			
			bool doSpringEffect = false;
			delta = Vector3.zero;
			
			// Chech for x-
			float springLimit = minLimit.x + springEffectAreaSize;
			if (rect.minX < springLimit)
			{
				delta.x = springLimit - rect.minX;
				doSpringEffect = true;
			}
			// Check for x+
			else
			{
				springLimit = maxLimit.x - springEffectAreaSize;
				if (rect.maxX > springLimit)
				{
					delta.x = springLimit - rect.maxX;
					doSpringEffect = true;
				}
			}
			
			// Chech for z-
			springLimit = minLimit.z + springEffectAreaSize;
			if (rect.minZ < springLimit)
			{
				delta.z = springLimit - rect.minZ;
				doSpringEffect = true;
			}
			// Check for z+
			else 
			{
				springLimit = maxLimit.z - springEffectAreaSize;
				if (rect.maxZ > springLimit)
				{
					delta.z = springLimit - rect.maxZ;
					doSpringEffect = true;
				}
			}
			
			delta *= springEffectSpeed * Time.deltaTime;
			
			return doSpringEffect;
		}
	}
}