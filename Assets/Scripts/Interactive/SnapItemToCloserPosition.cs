using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using Path;
using System;

namespace Interactive.Detail
{
	public class SnapItemToCloserPosition : MonoBehaviour 
	{
		[SerializeField]
		private float timeToSnap = 0.02f;

		[SerializeField]
		private Ease easeToSnap = Ease.InBounce;
        
		[SerializeField]
	    private Transform[] transformsToSnap;

		private bool keepInSamePlace = false;
		private Vector3 snapedPosition = Vector3.zero;
		private Transform myTransform;
		private Transform myParent;

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

		private void Start ()
		{
			myParent = transform.parent;
		}

        //TODO: Do not marry this shit with the class Node, find them by other media
	    private void FindPositionsToSnap() 
		{
	        Node[] nodes = FindObjectsOfType<Node>();
            transformsToSnap = new Transform[nodes.Length];
	        for (int i = 0; i < nodes.Length; i++) 
	            transformsToSnap[i] = nodes[i].transform;
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
					myTransform.SetParent(transformToSnap);
                    distance = distanceBetweenPoints;
                }
            }

			SnapToTransformPosition (transformToSnap);
	    }

		private void SnapToTransformPosition (Transform transformToSnap)
		{
			if (transformToSnap != null) 
			{
				myTransform.DOKill ();
				myTransform.DOLocalMove (Vector3.zero, timeToSnap).SetEase (easeToSnap).OnComplete (()=>ResetParent ());
				NodeSpnaped = transformToSnap.GetComponent<Node> ();
			}
		}

		private void ResetParent ()
		{
			myTransform.SetParent (myParent);
		}

		public void SnapInPlace ()
		{
			snapedPosition = myTransform.position;
			keepInSamePlace = true;
		}

		public void LateUpdate ()
		{
			if(keepInSamePlace)
				myTransform.position = snapedPosition;
		}
	}
}