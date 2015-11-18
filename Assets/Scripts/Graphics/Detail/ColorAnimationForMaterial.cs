using UnityEngine;
using System.Collections;

namespace Graphics.Detail
{
	public class ColorAnimationForMaterial : MonoBehaviour {
		public float duration = 1.0F;
		public Color color0 = Color.red;
		public Color color1 = Color.blue;
		public Material material;
		void Start() {
			material = GetComponent<Renderer>().material;
		}
		void Update() {
			float t = Mathf.PingPong(Time.time, duration) / duration;
			material.color = Color.Lerp(color0, color1, t);
		}
	}
}