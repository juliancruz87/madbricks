﻿using System;
using Assets.Scripts.Util;
using UnityEngine;
using Drag;
using Graphics;
using Map;
using System.Collections.Generic;
using ManagerInput;

namespace Interactive.Detail
{
    public class BeginStepRotationHelper : BeginStepGameBase {

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

  	    public override void StartStep() 
		{
			rotateAnimation.SetActive (true);
			rotatorCollider.enabled = true;
            handlerCollider.enabled = true;
            isStepActive = true;
            GameManager.Instance.GameStateChanged += CheckState;
        }

        private void CheckState(GameStates gameState)
        {
            if (gameState == GameStates.Play)
            {
                isStepActive = false;
                rotateAnimation.SetActive(false);
                handlePointerAnimation.SetActive(false);
                GameManager.Instance.GameStateChanged -= CheckState;
                EndStep();
            }
        }


		private void LateUpdate()
		{ 
            if (isStepActive)
                ToggleAnimations();  
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

            }
            else
            {
                rotateAnimation.SetActive(true);
                handlePointerAnimation.SetActive(false);
            }

        }
    }
}