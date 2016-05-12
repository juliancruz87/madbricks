using Map;
using Path;
using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using DG.Tweening;

namespace Interactive.Detail {

    public class BeginStepGrayscale : BeginStepGameBase
    {
        [SerializeField]
        private Shader grayscaleShader;

		[SerializeField]
		private Shader grayscaleAlphaShader;

        [SerializeField]
        private Material grayscaleSkybox;

        [SerializeField]
		private float duration;

        private Dictionary<int, Shader> shaderDictionary = new Dictionary<int, Shader>();

		private Sequence animationSequence;

        private Renderer [] renderers;
		private List<Renderer> excludedRenderers = new List<Renderer>();

        public override void StartStep()
        {
			List<MapObject> goals = MapObject.GetMapObjectsOfType (MapObjectType.Totem_target, MapObjectType.LauncherNormal, MapObjectType.LauncherSticky);
			GameObject[] excludedTotems = GameObject.FindGameObjectsWithTag ("TotemError");

            Skybox skybox = FindObjectOfType<Skybox>();

            skybox.material = grayscaleSkybox;

			foreach (MapObject goal in goals) 
			{
				excludedRenderers.AddRange (goal.gameObject.GetComponentsInChildren<Renderer> ());
			}

			foreach (GameObject totem in excludedTotems) 
			{
				excludedRenderers.AddRange (totem.GetComponentsInChildren<Renderer> ());
			}
				

			renderers = FindObjectsOfType<Renderer>();

			SetGrayscale();
            
			if (EndStep != null)
                EndStep();
        }

        private void SetGrayscale()
        {
			animationSequence = DOTween.Sequence ();

            for (int i = 0; i < renderers.Length; i++)
            {
				if (!excludedRenderers.Contains(renderers[i]))
                {
					if (renderers[i].material.HasProperty("_Mode") && renderers[i].material.GetFloat("_Mode") > 0)
						ApplyShader(renderers[i], grayscaleAlphaShader, "_EffectAmount", 0, 1, duration);
					else
						ApplyShader(renderers[i], grayscaleShader, "_Saturation", 1, 0, duration);
                }
            }

			animationSequence.AppendCallback (CompleteStep);
        }

        private void ApplyShader(Renderer renderer, Shader shader, string property, float initialValue, float endValue, float duration)
        {
            renderer.material.shader = shader;
			renderer.material.SetFloat (property, initialValue);
			animationSequence.Insert(0, renderer.material.DOFloat (endValue, property, duration));

        }
			
		private void CompleteStep ()
		{
			EndStep ();
		}	

        public void DeactivateGrayScale()
        {
            foreach (KeyValuePair<int, Shader> entry in shaderDictionary)
            {
                renderers[entry.Key].material.shader = entry.Value;
            }
        }
    }
}