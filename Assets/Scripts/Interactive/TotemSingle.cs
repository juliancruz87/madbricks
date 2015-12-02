using UnityEngine;
using System.Collections;
using Path;
using System.Collections.Generic;
using Interactive.Detail;
using Drag;
using DG.Tweening;
using Interactive;

namespace InteractiveObjects.Detail
{
	public class TotemSingle : MonoBehaviour
	{
		private SnapItemToCloserPosition snaper;
		private TotemInstantiatorConfig totem;
		private List<int> validStartPoints;
		private Transform myTransform;
		private bool isPlaying = false;
		private DraggableObject dragObject;

		private void Awake ()
		{
			snaper = GetComponent<SnapItemToCloserPosition> ();
			dragObject = GetComponent<DraggableObject> ();
			myTransform = transform;
		}

		public void SetUp (TotemInstantiatorConfig totem, List<int> validStartPoints)
		{
			this.totem = totem;
			this.validStartPoints = validStartPoints;
		}

		private void Update ()
		{
			if(GameManagerAccess.GameManagerState.CurrentState == GameStates.Play && !isPlaying)
			{
				isPlaying = true;
				Play ();
			}
		}

		private void Play ()
		{
			if(validStartPoints.Contains (dragObject.CurrentNode.Id))
				TryMove ();
			else
				EndGame ("Totem : " + name+ " cannot move");
		}

		private void TryMove ()
		{
			List<Node> nodes =  PathBuilder.Instance.Finder.GetNodes (snaper.NodeSpnaped, totem.PositionToGo);
			if (nodes.Count > 0)
			{
				GoToNode (0, nodes);
				GameManagerAccess.GameManagerState.Goal ();
			}
			else
				EndGame ("Totem : " + name + " could not found a path");
		}

		private void GoToNode (int currentNode, List<Node> nodes)
		{
			if(currentNode < nodes.Count)
			{
				Node node = nodes[currentNode];
				int nextNode = currentNode++;
				myTransform.DOMove (node.transform.position, totem.SpeedPerTile).OnComplete (() => GoToNode (nextNode, nodes));
			}
		}

		private void EndGame (string warning)
		{
			Debug.LogWarning (warning);
			GameManagerAccess.GameManagerState.Lose ();
		}
	}
}