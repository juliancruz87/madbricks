using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Map;
using InteractiveObjects;
using InteractiveObjects.Detail;
using Interactive.Totems;
using Path;

namespace Interactive.Detail
{
	public class TotemInstantiator : ScriptableObject
	{
		[SerializeField]
		private List<int> validStartPoints;

		[SerializeField]
		private List<TotemInstantiatorConfig> totems;
		private List<GameObject> totemsCreated = new List<GameObject> ();

		public List<ITotem> Totems
		{
			get
			{ 
				List<ITotem> totemsConverted = new List<ITotem> ();
				foreach (GameObject gameobject in totemsCreated)
				{
					ITotem createdTotem = gameobject.GetComponent<ITotem> ();
					totemsConverted.Add (createdTotem);
				}
				return totemsConverted;
			}
		}

		public int TotemsNum
		{
			get { return totems.Count; }
		}

		public void Instantiate ( List<Node> points, Transform positionsToSnap , Transform parent)
		{
			totemsCreated = new List<GameObject> ();

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

		private void InitializeComponents (GameObject gameObjectTotem, TotemInstantiatorConfig totem, List<Node> points, Transform positionsToSnap)
		{
			MapObject mapObject = gameObjectTotem.GetComponent<MapObject> ();
			Transform startPosition = CreateStartPosition (points, totem.PositionToAdd);
			mapObject.SetStartPosition (startPosition);
			AddComponent (gameObjectTotem, totem);
		}

		private void AddComponent (GameObject gameObjectTotem, TotemInstantiatorConfig totem)
		{
			if (HasAddedTotemComponents (gameObjectTotem, totem)) 
			{
				Totem totemObject = gameObjectTotem.GetComponent<Totem> ();
				totemObject.SetUp (totem, validStartPoints);
				AddTotemBehaviours(gameObjectTotem, totem);
			}
		}

		private bool HasAddedTotemComponents (GameObject gameObjectTotem, TotemInstantiatorConfig totem)
		{
			if (totem.Type == TotemType.Square)
				gameObjectTotem.AddComponent<SquareTotem> ();
			else if (totem.Type == TotemType.Triangle)
				gameObjectTotem.AddComponent<TriangleTotem> ();
			else if (totem.Type == TotemType.Sphere)
				gameObjectTotem.AddComponent<SphereTotem> ();
			else
				return false;

			return true;
		}

		private void AddTotemBehaviours (GameObject gameObjectTotem, TotemInstantiatorConfig totem)
		{
			foreach (TotemBehaviours behavioursTypes in totem.BehavioursTypes) 
			{
				if(behavioursTypes == TotemBehaviours.Explosive)
					gameObjectTotem.AddComponent<TotemExplosive> ();
				else if(behavioursTypes == TotemBehaviours.HoleFiller)
					gameObjectTotem.AddComponent<TotemHoleFiller> ();
				else if(behavioursTypes == TotemBehaviours.Phantom)
					gameObjectTotem.AddComponent<TotemPhantom> ();
			}
		}

		private Transform CreateStartPosition(List<Node> points, int positionToAdd)
		{
			Node positionToInstantiate = points.Find (c=> c.Id == positionToAdd );
			return positionToInstantiate.transform;
		}
	}
}