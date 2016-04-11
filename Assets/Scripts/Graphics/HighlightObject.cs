using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;

namespace Graphics
{
	public class HighlightObject : MonoBehaviour
	{
		[SerializeField]
		private Shader outlineShader;

		[SerializeField]
		private HighlightShaderSettings settings;

		[SerializeField]
		private bool isAnimated;

		[SerializeField]
		private float animationDuration = 0.5f;

		private Shader originalShader;
		private Renderer myRenderer;
		private bool isActive;

		private void Start()
		{
			myRenderer = gameObject.GetComponentInChildren<Renderer> ();
		}

		private void StartHighlight()
		{
			isActive = true;
			originalShader = myRenderer.material.shader;
			myRenderer.material.shader = outlineShader;
			SetUpMaterial (myRenderer.material);

			if (isAnimated) 
				AnimateProperties (myRenderer.material);
			
		}

		public void ActivateHighlight()
		{
			if (!isActive)
				StartHighlight ();

		} 

		public void DeactivateHighlight()
		{
			isActive = false;

			if(originalShader != null)
				myRenderer.material.shader = originalShader;

			if (isAnimated)
				DOTween.Kill (myRenderer.material);
		}

		private void AnimateProperties(Material material)
		{
			foreach (ShaderColorProperty property in settings.ColorProperties)
				material.DOColor (property.maxValue, property.name, animationDuration).SetLoops(-1, LoopType.Yoyo);

			foreach (ShaderFloatProperty property in settings.FloatProperties)
				material.DOFloat (property.maxValue, property.name, animationDuration).SetLoops(-1, LoopType.Yoyo);
		}

		private void SetUpMaterial(Material material)
		{
			foreach (ShaderColorProperty property in settings.ColorProperties)
				material.SetColor(property.name, property.minValue);

			foreach (ShaderFloatProperty property in settings.FloatProperties)
				material.SetFloat(property.name, property.minValue);
		}


	}
}