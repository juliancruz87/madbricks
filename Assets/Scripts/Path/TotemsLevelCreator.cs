using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Interactive.Detail
{
	public class TotemsLevelCreator : MonoBehaviour 
	{
		[SerializeField]
		private Transform grid;

		[SerializeField]
		private TotemInstantiator instantiator;

		private List<Transform> points = new List<Transform>();

		private void Start ()
		{
			Transform [] childs = GetComponentsInChildren<Transform> ();
			System.Array.ForEach (childs, c => points.Add (c));

			instantiator.Instantiate (points, grid, grid);
		}
	}
}