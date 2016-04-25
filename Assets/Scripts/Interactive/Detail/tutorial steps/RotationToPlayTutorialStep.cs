using System;
using Assets.Scripts.Util;
using UnityEngine;
using Drag;
using Graphics;
using Map;
using System.Collections.Generic;
using ManagerInput;
using UnityEngine.UI;

namespace Interactive.Detail
{
	public class RotationToPlayTutorialStep : BeginStepGameBase
    {

        private const float handlerAngle = 135;
        private const float minWorldAngle = 170;
        private const float maxWorldAngle = 280;

        [SerializeField]
		private GameObject rotateAnimation;

        [SerializeField]
		private GameObject handlePointerAnimation;

        [SerializeField]
		private Collider rotatorCollider;

        [SerializeField]
        private Collider handlerCollider;

        [SerializeField]
		private Transform world;

        private bool isStepActive;
		private bool stepEnded;

		private void Start()
		{
            rotateAnimation.SetActive(false);
            handlePointerAnimation.SetActive(false);
            isStepActive = false;
			stepEnded = false;
		}

        public override void StartStep() 
		{
			isStepActive = true;
			rotateAnimation.SetActive (true);
			SetActiveColliders (true);
			GameManager.Instance.GameStateChanged += CheckState;
        }
			
        protected void DeactivateStep()
        {
            isStepActive = false;
			rotateAnimation.SetActive(false);
			handlePointerAnimation.SetActive(false);
        }

		private void Update()
		{
			if (!isStepActive && !stepEnded)
				SetActiveColliders (false);
		}

        private void LateUpdate()
		{
			if (isStepActive)
				ToggleAnimations ();
		}

		private void SetActiveColliders(bool value)
		{
			rotatorCollider.enabled = value;
			handlerCollider.enabled = value;
		}

		private void CheckState(GameStates gameState)
		{
			if (gameState == GameStates.Play)
				DeactivateStep();
		}



        private void ToggleAnimations()
        {
            float worldRotationAngle = world.eulerAngles.y;

            if (worldRotationAngle < maxWorldAngle && worldRotationAngle > minWorldAngle )
            {
                rotateAnimation.SetActive(false);
                handlePointerAnimation.SetActive(true);

                Vector3 rotation = handlePointerAnimation.transform.eulerAngles;
                rotation.y = handlerAngle;

                handlePointerAnimation.transform.eulerAngles = rotation;

                if (!stepEnded)
                {
                    stepEnded = true;
                    EndStep();
                }
            }
            else
            {
                rotateAnimation.SetActive(true);
                handlePointerAnimation.SetActive(false);
            }

        }

    }
}