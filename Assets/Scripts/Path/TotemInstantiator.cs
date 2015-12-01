﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Map;
using InteractiveObjects;

namespace Interactive.Detail
{
	public class TotemInstantiator : ScriptableObject
	{
		[SerializeField]
		private List<int> validStartPoints;
		[SerializeField]
		private List<TotemInstantiatorConfig> totems;

		public void Instantiate ( List<Transform> points, Transform positionsToSnap , Transform parent)
		{
			foreach (TotemInstantiatorConfig totem in totems) 
			{
				GameObject gameObjectTotem = Instantiate<GameObject> (totem.Prefab);
				if(gameObjectTotem == null)
					Debug.LogWarning ("There isn't prefab set in totem instantiator");
				else
				{
					InitializeComponents (gameObjectTotem, totem, points, positionsToSnap);
					gameObjectTotem.transform.parent = parent;
				}
			}
		}

		private void InitializeComponents (GameObject gameObjectTotem, TotemInstantiatorConfig totem, List<Transform> points, Transform positionsToSnap)
		{
			MapObject mapObject = gameObjectTotem.GetComponent<MapObject> ();
			mapObject.SetStartPosition (CreateStartPosition (points, totem.PositionToAdd));

			ITotemMovesController moveController = gameObjectTotem.GetComponent<ITotemMovesController> ();
			moveController.CreatePositionsToSnap (positionsToSnap);
		}

		private Transform CreateStartPosition(List<Transform> points, int positionToAdd)
		{
			Transform positionToInstantiate = points.Find (c=> c.name.Contains(positionToAdd.ToString ()));
			return positionToInstantiate;
		}
	}
}