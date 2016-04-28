using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI;

namespace Interactive.Detail
{
	public class ShowPathTutorialStep : BeginStepGameBase 
	{
		[SerializeField]
		private GameObject pathPrefab;

        [SerializeField]
        private int totemPosition;

        [SerializeField]
        private Transform worldTransform;

        [SerializeField]
        private Color hintColor;

        [SerializeField]
        private float checkAngle = 0;

        private ITotem totem;
        protected Vector3[] positions;

        private Transform totemTransform;

        private LineHintPainter pathPainter;

        private bool hasToRepaint;

        private bool stepActive;

        public override void StartStep()
        {
            GameObject go = Instantiate(pathPrefab);
            totem = GetTotem(totemPosition);
            totemTransform = totem.DragObject.gameObject.transform;

            GetPositions();
            pathPainter = go.GetComponent<LineHintPainter>();
            SetHintPainter();

            hasToRepaint = true;
            stepActive = true;

            GameManager.Instance.GameStateChanged += DeactivateStep;
            EndStep();
        }

        protected virtual void GetPositions()
        {
            positions = totem.GetPathPositions();
        }

        private void Update()
        {
            if (stepActive)
                CheckRepaint();
        }

        private void CheckRepaint()
        {
            if (totem.CurrentNode.Id == totemPosition && totemTransform.localEulerAngles.y == checkAngle)
            {
                if (hasToRepaint && !totem.IsDragged)
                {
                    hasToRepaint = false;
                    pathPainter.Paint(positions);
                }
            }
            else
            {
                hasToRepaint = true;
                pathPainter.Erase();
            }    
        }

        private void DeactivateStep(GameStates gameState)
        {
            if (gameState == GameStates.Play)
                stepActive = false;
        }

        private void SetHintPainter()
        {
            pathPainter.TransformParent = worldTransform;
            pathPainter.Color = hintColor;
        }

        private ITotem GetTotem(int totemPosition)
        {
            List<ITotem>totems = GameManager.Instance.Totems;

            return totems.Find(totem => totem.CurrentNode.Id == totemPosition);
        }	
    }
}