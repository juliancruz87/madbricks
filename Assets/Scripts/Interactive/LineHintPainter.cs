using System;
using UnityEngine;

namespace Interactive
{
	public class LineHintPainter : MonoBehaviour
    {
        private Transform transformParent;
        private LineRenderer lineRenderer;
        private Color color;

        public Transform TransformParent
        {
            set { transformParent = value; }
        }

        public Color Color
        {
            set
            {
                color = value;
                lineRenderer.material.SetColor("_Color", color);
            }
        }

        private void Awake()
        {
            lineRenderer = GetComponentInChildren<LineRenderer>();
        }

        public void Paint(Vector3[] positions)
        {
            gameObject.transform.parent = gameObject.transform.root;
            lineRenderer.SetVertexCount(positions.Length);
            lineRenderer.SetPositions(positions);
            gameObject.transform.parent = transformParent;
        }

        public void Erase()
        {
            lineRenderer.SetVertexCount(0);
        }
	}
	
}