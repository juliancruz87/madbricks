using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Path;
using System;

namespace InteractiveObjects.Detail
{
	public class SnapItemToCloserPosition : MonoBehaviour 
	{
		[SerializeField]
		private float timeToSnap = 0.75f;
		[SerializeField]
		private Ease easeToSnap = Ease.InBounce;
        [SerializeField]
	    private Transform[] transformsToSnap;

		private Transform myTransform;

		public Node NodeSpnaped
		{
			get;
			private set;
		}

	    private void Awake() 
		{
            myTransform = GetComponent<Transform>();
	        FindPositionsToSnap();
	    }

        //TODO: Do not marry this shit with the class Node, find them by other media
	    private void FindPositionsToSnap() 
		{
	        Node[] nodes = FindObjectsOfType<Node>();
            transformsToSnap = new Transform[nodes.Length];
	        for (int i = 0; i < nodes.Length; i++) 
			{
	            transformsToSnap[i] = nodes[i].transform;
	        }
	    }

	    public void SnapToCloserTransform() 
		{
            float distance = float.MaxValue;
            Transform transformToSnap = null;

            foreach (Transform transform in transformsToSnap) 
			{
                float distanceBetweenPoints = Vector3.Distance(myTransform.position, transform.position);
                if (distanceBetweenPoints < distance) 
				{
                    transformToSnap = transform;
                    distance = distanceBetweenPoints;
                }
            }

			SnapToTransformPosition (transformToSnap);
	    }

		private void SnapToTransformPosition (Transform transformToSnap)
		{
			if (transformToSnap != null) 
			{
				myTransform.DOMove (transformToSnap.position, timeToSnap).SetEase (easeToSnap);
				NodeSpnaped = transformToSnap.GetComponent<Node> ();
			}
		}
	}
}