using Drag;
using Path;
using UnityEngine;
using DG.Tweening;
using Interactive;
using Interactive.Detail;
using System.Collections;
using System.Collections.Generic;
using Sound;

namespace InteractiveObjects.Detail
{
	public abstract class Totem : MonoBehaviour
	{
		private Transform myTransform;
		private List<int> validStartPoints;
		private DraggableObject dragObject;
		private IGameManagerForStates gameStates;
		private TotemControllerStop controllerToStop;
		private SnapItemToCloserPosition snaper;

		protected TotemInstantiatorConfig totem;
		private bool canMove = false;

		protected Node CurrentNode 
		{
			get{ return dragObject.CurrentNode;}
		}

		private void Awake ()
		{
			snaper = GetComponent<SnapItemToCloserPosition> ();
			dragObject = GetComponent<DraggableObject> ();
			controllerToStop = GetComponent<TotemControllerStop> ();
			myTransform = transform;
		}
		
		public void SetUp (TotemInstantiatorConfig totem, List<int> validStartPoints, IGameManagerForStates gameStates)
		{
			this.gameStates = gameStates;
			this.totem = totem;
			this.validStartPoints = validStartPoints;
			gameStates.StartedGame += OnStartedGame;
			controllerToStop.CollidedWithTotem += OnCrashWithOtherCollider;
			SetGameManagerForUI (gameStates);
		}

		private void OnCrashWithOtherCollider ()
		{
			canMove = false;
			myTransform.DOKill ();
			snaper.SnapToCloserTransform ();
		}

		private void SetGameManagerForUI (IGameManagerForStates gameStates)
		{
			SetterGameManagerForStates setter = GetComponent<SetterGameManagerForStates> ();
			if (setter != null)
				setter.GameManager = gameStates;
		}
		
		private void OnDestroy ()
		{
			if (gameStates != null) 
			{
				gameStates.StartedGame -= OnStartedGame;
				controllerToStop.CollidedWithTotem -= OnCrashWithOtherCollider;
			}
		}
		
		private void OnStartedGame ()
		{
			if(dragObject.CurrentNode != null && validStartPoints.Contains (dragObject.CurrentNode.Id))
			{
				canMove = true;
				controllerToStop.TurnOnColliderToDetect ();
				Move ();
			}
			else
				EndGame ("Totem : " + name+ " cannot move");
		}

		protected void GoToNode (int currentNode, List<Node> nodes)
		{
			if(!canMove)
				return;

			if(currentNode < nodes.Count)
			{
				Node node = nodes[currentNode];
				int nextNode = currentNode++;
				bool hasBeenReached = CheckIfHaBeenArrivedToHisDestiny (node);
				myTransform.DOMove (node.transform.position, totem.SpeedPerTile).OnComplete (() => GetReachedToPoint(nextNode, nodes, hasBeenReached));
			}
		}
		
		private void GetReachedToPoint (int nextNode, List<Node> nodes, bool hasBeenReached)
		{
			if (hasBeenReached && totem.SoundToGetReach != null)
				SoundManager.Instance.Play (totem.SoundToGetReach);
			
			GoToNode (nextNode, nodes);
		}
		
		private bool CheckIfHaBeenArrivedToHisDestiny (Node node)
		{
			if (node.Id == totem.PositionToGo) 
			{
				gameStates.Goal ();
				return true;
			}
			return false;
		}
		
		protected void EndGame (string warning)
		{
			Debug.LogWarning (warning);
			gameStates.Lose ();
		}

		protected abstract void Move ();
	}
}