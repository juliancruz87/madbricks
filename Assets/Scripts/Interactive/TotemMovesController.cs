using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace InteractiveObjects.Detail
{
	public class TotemMovesController : MonoBehaviour 
	{
		[SerializeField]
		private DragableItem dragable;
		[SerializeField]
		private SnapItemToCloserPosition snaper;
		[SerializeField]
		private Transform parentToPositionsToSnap;
		private List<Vector3> positionsToSnap = new List<Vector3> ();

		private void Start ()
		{
			Transform [] positions = parentToPositionsToSnap.GetComponentsInChildren<Transform> ();
			System.Array.ForEach (positions, p=> positionsToSnap.Add(p.position));
			dragable.Release += OnRelease;
		}

		private void OnDestroy ()
		{
			dragable.Release -= OnRelease;
		}

		private void OnRelease ()
		{
			snaper.SnapToCloserPosition (positionsToSnap);
		}
	}
}