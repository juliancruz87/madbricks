using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Path;

namespace InteractiveObjects.Detail
{
	public class TotemMovesController : MonoBehaviour, ITotemMovesController
	{
		[SerializeField]
		private DragableItem dragable;
		[SerializeField]
		private SnapItemToCloserPosition snaper;
		[SerializeField]
		private Transform parentToPositionsToSnap;
		private List<Transform> points = new List<Transform> (); 
		private List<Vector3> PositionsToSnap
		{
			get{ return points.ConvertAll(p => p.position);}
		}

		private void Start ()
		{
			CreatePositionsToSnap (parentToPositionsToSnap);
			dragable.Release += OnRelease;
		}

		public void CreatePositionsToSnap (Transform parentForPositionsToSnap)
		{
			if (parentForPositionsToSnap != null) 
			{
				points = new List<Transform> (); 
				parentToPositionsToSnap = parentForPositionsToSnap;
				Node [] positions = parentToPositionsToSnap.GetComponentsInChildren<Node> ();
				System.Array.ForEach (positions, p => points.Add (p.transform));
			}
		}

		private void OnDestroy ()
		{
			dragable.Release -= OnRelease;
		}

		private void OnRelease ()
		{
			snaper.SnapToCloserPosition (PositionsToSnap);
		}
	}
}