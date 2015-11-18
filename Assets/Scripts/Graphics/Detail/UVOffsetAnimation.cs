using UnityEngine;
using System.Collections;

namespace Graphics.Detail
{
	[System.Serializable]
	public class UVOffsetAnimation : UVAnimationBase
	{
		[SerializeField]
		private float horizontalSpeed = 0.1f;

		[SerializeField]
		private float verticalSpeed = 0.1f;

		protected override void Update()
		{
			if (isEnabled) {
				Vector2 offset = material.mainTextureOffset;
				material.mainTextureOffset = new Vector2 (offset.x + horizontalSpeed * Time.deltaTime, offset.y + verticalSpeed * Time.deltaTime);
			}
		}	
	}
}