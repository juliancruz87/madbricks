using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Path;

namespace Interactive.Detail
{
	public class TotemMovesController : MonoBehaviour, ITotemMovesController
	{
		[SerializeField]
		private DragableItem dragable;
		[SerializeField]
		private SnapItemToCloserPosition snaper;
		[SerializeField]
		private Transform parentToPositionsToSnap;

		private Node nodeSnaped;
		private List<Transform> points = new List<Transform> (); 

		private List<Vector3> PositionsToSnap
		{
			get
			{ 
				List<Vector3> positions = new List<Vector3> ();
				foreach (Transform point in points)
					positions.Add (point.position);
				return positions;
			}
		}

		private void Start ()
		{
			CreatePositionsToSnap (parentToPositionsToSnap);
		}

		public void CreatePositionsToSnap (Transform parentForPositionsToSnap)
		{
			if (parentForPositionsToSnap != null) 
			{
				points = new List<Transform> (); 
				parentToPositionsToSnap = parentForPositionsToSnap;
				Node [] positions = parentToPositionsToSnap.GetComponentsInChildren<Node> ();
				foreach (Node node in positions)
					points.Add (node.transform);
			}
		}
	}
}