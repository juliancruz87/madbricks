using UnityEngine;
using System.Collections;

namespace Graphics.Detail
{
	public class ColorAnimationForMaterial : MonoBehaviour 
	{
		[SerializeField]
		private float duration = 1.0F;

		[SerializeField]
		private Color color0 = Color.red;

		[SerializeField]
		private Color color1 = Color.blue;

		private  Material material;

		private void Start() 
		{
			material = GetComponent<Renderer>().material;
		}

		private void Update() 
		{
			float t = Mathf.PingPong(Time.time, duration) / duration;
			material.color = Color.Lerp(color0, color1, t);
		}
	}
}