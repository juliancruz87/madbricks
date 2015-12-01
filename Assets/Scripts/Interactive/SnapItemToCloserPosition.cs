using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

namespace InteractiveObjects.Detail
{
	public class SnapItemToCloserPosition : MonoBehaviour 
	{
		[SerializeField]
		private float timeToSnap = 0.75f;
		[SerializeField]
		private Ease easeToSnap = Ease.InBounce;

		private Transform myTransform;

		private void Start ()
		{
			myTransform = GetComponent<Transform> ();
		}

		public void SnapToCloserPosition (List<Vector3> positions)
		{
			float distance = 1000F;
			Vector3 positionToGo;

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