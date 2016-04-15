using System;
using Assets.Scripts.Util;
using UnityEngine;
using Drag;
using Graphics;
using Map;
using System.Collections.Generic;
using UnityEngine.UI;

namespace Interactive.Detail
{
    public abstract class TutorialStepBase : BeginStepGameBase {

        [SerializeField]
        private GameObject textField;

        [SerializeField]
        private TutorialTextSettings textSettings;

        private Text textComp;

        private void Awake()
        {
            textField.SetActive(false);
        }

        public override void StartStep() 
		{
			GameManager.Instance.GameStateChanged += CheckState;
            textField.SetActive(true);
            textComp = textField.GetComponentInChildren<Text>();
            BeginTutorialStep();

        }

        protected void ShowStartText()
        {
            ToggleText(textSettings.StartText);
        }

        protected void ShowIdleText()
        {
            ToggleText(textSettings.IdleText);
        }

        protected void ShowActionText()
        {
            ToggleText(textSettings.ActionText);
        }

        protected abstract void BeginTutorialStep();
		
		protected virtual void CompleteStep()
		{
			GameManager.Instance.GameStateChanged -= CheckState;
            textField.SetActive(false);
			EndStep ();
		}

		private void CheckState(GameStates gameState)
		{
			if (gameState == GameStates.Play)
				CompleteStep();
		}

        private void ToggleText(string text)
        {
            if (text != string.Empty)
            {
                textField.SetActive(true);
                textComp.text = text;

            }
            else
            {
                textField.SetActive(false);
            }
        }
    }
}