using UnityEngine;
using System.Collections;

namespace Graphics.Detail
{
	public abstract class UVAnimationBase : MonoBehaviour
	{
		[SerializeField]
		protected bool isEnabled = false;
		protected Material material;
		
		protected virtual void Start()
		{
			material = GetComponent<MeshRenderer> ().material;
			
			if (material == null)
			{
				Debug.LogWarning("A material is missing for uv animation"); 
				isEnabled = false;
			}
		}
		
		protected abstract void Update();
	}
}