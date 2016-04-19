using UnityEngine;

namespace Interactive
{
	public class LineHintPainter : MonoBehaviour
    {
        [SerializeField]
        Transform transformParent;

        private LineRenderer lineRenderer;

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