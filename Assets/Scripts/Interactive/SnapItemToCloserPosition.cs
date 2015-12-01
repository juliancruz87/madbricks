using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Path;

namespace InteractiveObjects.Detail
{
	public class SnapItemToCloserPosition : MonoBehaviour {
		[SerializeField]
		private float timeToSnap = 0.75f;
		[SerializeField]
		private Ease easeToSnap = Ease.InBounce;
        [SerializeField]
	    private Transform[] transformsToSnap;

		private Transform myTransform;

	    private void Awake() {
            myTransform = GetComponent<Transform>();
	        FindPositionsToSnap();
	    }

        //TODO: Do not marry this shit with the class Node, find them by other media
	    private void FindPositionsToSnap() {
	        Node[] nodes = FindObjectsOfType<Node>();
            transformsToSnap = new Transform[nodes.Length];
	        for (int i = 0; i < nodes.Length; i++) {
	            transformsToSnap[i] = nodes[i].transform;
	        }
	    }

	    public void SnapToCloserTransform() {
            float distance = float.MaxValue;
            Transform transformToSnap = null;

            foreach (Transform transform in transformsToSnap) {
                float distanceBetweenPoints = Vector3.Distance(myTransform.position, transform.position);
                if (distanceBetweenPoints < distance) {
                    transformToSnap = transform;
                    distance = distanceBetweenPoints;
                }
            }

            if (transformToSnap != null)
                myTransform.DOMove(transformToSnap.position, timeToSnap).SetEase(easeToSnap);
	    }

		public void SnapToCloserPosition (List<Vector3> positions) {
			float distance = 1000F;
			Vector3 positionToGo =  new Vector3();

			foreach (Vector3 position in positions)
			{
				float distanceBetweenPoints = Vector3.Distance (myTransform.position, position);
				if(distanceBetweenPoints < distance)
				{
					positionToGo = position;
					distance = distanceBetweenPoints;
				}
			}

			myTransform.DOMove(positionToGo, timeToSnap).SetEase (easeToSnap);
		} 
	}
}