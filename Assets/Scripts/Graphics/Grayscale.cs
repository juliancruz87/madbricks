using UnityEngine;
using System.Collections;
using DG.Tweening;
using System;
using System.Collections.Generic;

namespace Graphics
{
	public class Grayscale : MonoBehaviour
	{
		[SerializeField]
		private Shader grayscaleShader;

		private List<Shader> originalShader;
        private Renderer[] renderers;

        public void TurnGrayscale()
        {
            renderers = FindObjectsOfType<Renderer>();
            foreach (Renderer renderer in renderers)
            {
                renderer.material.shader = grayscaleShader;
            }
        }
	}
}