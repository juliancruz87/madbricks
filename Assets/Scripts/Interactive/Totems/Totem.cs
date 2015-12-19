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
		private DraggableObject dragObject;
		private IGameManagerForStates gameStates;
		private TotemControllerStop controllerToStop;
		private SnapItemToCloserPosition snaper;
		private List<int> validStartPoints;

		protected Transform myTransform;
		protected TotemInstantiatorConfig totem;

		protected bool IsInStartPoint
		{
			get { return dragObject.CurrentNode != null && validStartPoints != null && validStartPoints.Contains (dragObject.CurrentNode.Id); }
		}

		protected PathBuilderFinder Finder 
		{
			get { return PathBuilder.Instance.Finder; }
		}

		protected Node CurrentNode 
		{
			get{ return dragObject.CurrentNode;}
		}

		protected virtual void Awake ()
		{
			myTransform = transform;
			snaper = GetComponent<SnapItemToCloserPosition> ();
			dragObject = GetComponent<DraggableObject> ();
			controllerToStop = GetComponent<TotemControllerStop> ();
		}
		
		public void SetUp (TotemInstantiatorConfig totem, List<int> validStartPoints, IGameManagerForStates gameStates)
		{
			if (totem.Type == TotemType.Square || totem.Type == TotemType.Triangle)  
			{
				this.gameStates = gameStates;
				this.totem = totem;
				this.validStartPoints = validStartPoints;
				gameStates.StartedGame += OnStartedGame;
				controllerToStop.CollidedWithTotem += OnCrashWithOtherCollider;
				SetGameManagerForUI (gameStates);
			}
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
			if(IsInStartPoint)
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

			snaper.SnapInPlace ();
		}

		protected void EndGame (string warning)
		{
			Debug.LogWarning (warning);
			gameStates.Lose ();
		}

		protected virtual void Update ()
		{
#if UNITY_EDITOR
			Vector3 position = myTransform.position;
			Debug.DrawLine(position , position - (myTransform.forward * 0.1F), Color.red);
			Debug.DrawLine(position , position - ((myTransform.forward *-1f) * 0.1F), Color.blue);
			Debug.DrawLine(position , position - ((myTransform.right *-1f) * 0.1F), Color.yellow);
			Debug.DrawLine(position , position - (myTransform.right * 0.1F), Color.green);
#endif
		}

		protected abstract void GetReachedToPoint (Node node);
		protected abstract void Move ();
	}
}