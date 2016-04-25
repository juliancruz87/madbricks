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

        private ITotem totem;

        private LineHintPainter pathPainter;

        public override void StartStep()
        {
            GameObject go = Instantiate(pathPrefab);
            totem = GetTotem(totemPosition);

            pathPainter = go.GetComponent<LineHintPainter>();
            SetHintPainter(totem.GetPathPositions());

            EndStep();
        }

        private void SetHintPainter(Vector3[] positions)
        {
            pathPainter.TransformParent = worldTransform;
            pathPainter.Color = hintColor;
            pathPainter.Paint(totem.GetPathPositions());
        }

        private ITotem GetTotem(int totemPosition)
        {
            List<ITotem>totems = GameManager.Instance.Totems;

            return totems.Find(totem => totem.CurrentNode.Id == totemPosition);
        }	
    }
}