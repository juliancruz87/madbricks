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
    public class RotationToPlayTutorialStep : TutorialStepBase
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
        private bool isInIdle;

        protected override void BeginTutorialStep() 
		{
			rotateAnimation.SetActive (true);
			rotatorCollider.enabled = true;
            handlerCollider.enabled = true;
            isStepActive = true;
            isInIdle = false;
            GameManager.Instance.GameStateChanged += CheckState;
            ShowStartText();
        }

        private void CheckState(GameStates gameState)
        {
            if (gameState == GameStates.Play)
                CompleteStep();
        }

        protected override void CompleteStep()
        {
            isStepActive = false;
            rotateAnimation.SetActive(false);
            handlePointerAnimation.SetActive(false);
            GameManager.Instance.GameStateChanged -= CheckState;
            base.CompleteStep();
        }

        private void LateUpdate()
		{
            if (!isInIdle && TouchChecker.IsTouchingFromCollider(Camera.main, rotatorCollider))
                isInIdle = true;
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
                ShowActionText();
            }
            else
            {
                CheckIdleText();
                rotateAnimation.SetActive(true);
                handlePointerAnimation.SetActive(false);
            }

        }
        private void CheckIdleText()
        {
            if (isInIdle)
                ShowIdleText();
        }

    }
}