﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Map;
using InteractiveObjects;
using InteractiveObjects.Detail;
using Zenject;

namespace Interactive.Detail
{
	public class TotemInstantiator : ScriptableObject
	{
		[SerializeField]
		private List<int> validStartPoints;

		[SerializeField]
		private List<TotemInstantiatorConfig> totems;

		public IGameManagerForStates GameStates 
		{
			set;
			private get;
		}

		public int TotemsNum
		{
			get { return totems.Count; }
		}

		public void Instantiate ( List<Transform> points, Transform positionsToSnap , Transform parent)
		{
			List<GameObject> totemsCreated = new List<GameObject> ();

			foreach (TotemInstantiatorConfig totem in totems) 
			{
				GameObject gameObjectTotem = Instantiate<GameObject> (totem.Prefab);
				if (gameObjectTotem != null)
				{
					InitializeComponents (gameObjectTotem, totem, points, positionsToSnap);
					gameObjectTotem.transform.parent = parent;
					totemsCreated.Add (gameObjectTotem);
				}
			}

			foreach (GameObject totem in totemsCreated) 
			{
				TotemControllerStop totemController = totem.GetComponent<TotemControllerStop> ();
				if(totemController != null)
					totemController.SetTotems (totemsCreated);
			}
		}

		private void InitializeComponents (GameObject gameObjectTotem, TotemInstantiatorConfig totem, List<Transform> points, Transform positionsToSnap)
		{
			MapObject mapObject = gameObjectTotem.GetComponent<MapObject> ();
			mapObject.SetStartPosition (CreateStartPosition (points, totem.PositionToAdd));

			AddComponent (gameObjectTotem, totem);
		}

		private void AddComponent (GameObject gameObjectTotem, TotemInstantiatorConfig totem)
		{
			if (totem.Type == TotemType.Single) 
			{
				gameObjectTotem.AddComponent <TotemSingle>();
				TotemSingle totemObject = gameObjectTotem.GetComponent<TotemSingle> ();
				totemObject.SetUp (totem, validStartPoints, GameStates);
			}
		}

		private Transform CreateStartPosition(List<Transform> points, int positionToAdd)
		{
			Transform positionToInstantiate = points.Find (c=> c.name.Contains(positionToAdd.ToString ()));
			return positionToInstantiate;
		}
	}
}