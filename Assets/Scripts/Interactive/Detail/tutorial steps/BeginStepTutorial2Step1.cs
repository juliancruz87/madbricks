using System;
using Assets.Scripts.Util;
using UnityEngine;
using Drag;

namespace Interactive.Detail
{
    public class BeginStepTutorial2Step1 : BeginStepGameBase {

        [SerializeField]
		private int activeTotemPosition;
      	[SerializeField]
		private Shader highlightShader;

		private Totem[] totems;

		private Totem activeTotem;
		private Shader originalShader;
		private Renderer totemRenderer;

        public override void StartStep() 
		{
			totems = FindObjectsOfType<Totem>(); 
			ConfigTotems ();
			totemRenderer = activeTotem.gameObject.GetComponentInChildren<Renderer>();
			originalShader = totemRenderer.material.shader;
			totemRenderer.material.shader = highlightShader;

        }

		private void ConfigTotems () 
		{
			foreach(Totem totem in totems)
			{
				if (totem.InitialPosition == activeTotemPosition) 
				{
					SetToggleTotem (totem, true);
					activeTotem = totem;
				}
				else
					SetToggleTotem (totem, false);
			}
		}

		private void SetToggleTotem(Totem totem, bool canBeDragged)
		{
			totem.GetComponent<DraggableObject>().CanBeDragged = canBeDragged;
		}


        private void Update() {

        }

    }
}