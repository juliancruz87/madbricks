using Path;
using Drag;
using Sound;
using UnityEngine;
using Interactive;
using DG.Tweening;
using System.Collections.Generic;

namespace Interactive.Detail
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

		protected Node CurrentNode 
		{
			get{ return dragObject.CurrentNode;}
		}

		private void Awake ()
		{
			myTransform = transform;
			snaper = GetComponent<SnapItemToCloserPosition> ();
			dragObject = GetComponent<DraggableObject> ();
			controllerToStop = GetComponent<TotemControllerStop> ();
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
			myTransform.DOKill ();
			snaper.SnapToCloserTransform ();
			EndGame ("Has been crashed with other totem");
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
				controllerToStop.TurnOnColliderToDetect ();
				Move ();
			}
			else
				EndGame ("Totem : " + name+ " cannot move");
		}

		protected void GoToNode (Node node, float speed)
		{
			myTransform.DOMove (node.transform.position, speed).OnComplete (() => GetReachedToPoint(node));
		}

		protected void GoalReachedNode(Node node)
		{
			if (node.Id == totem.PositionToGo)
				gameStates.Goal ();
			
			if(totem.SoundToGetReach != null) 
				SoundManager.Instance.Play (totem.SoundToGetReach);
		}

		protected void EndGame (string warning)
		{
			Debug.LogWarning (warning);
			gameStates.Lose ();
		}

		protected abstract void GetReachedToPoint (Node node);
		protected abstract void Move ();
	}
}