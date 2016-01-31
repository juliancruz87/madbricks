using System;
using Path;
using Drag;
using Sound;
using UnityEngine;
using Interactive;
using DG.Tweening;
using System.Collections.Generic;

namespace Interactive.Detail
{
	public abstract class Totem : MonoBehaviour, ITotem
	{
		private DraggableObject dragObject;
		private TotemControllerStop controllerToStop;
		private SnapItemToCloserPosition snaper;
		private List<int> validStartPoints;
		private List<TotemType> validTypes = new List<TotemType> ();

		protected Transform myTransform;
		protected TotemInstantiatorConfig totem;
	    public abstract TotemType Type { get; }

		protected IGameManagerForStates GameManagerForStates
		{
			get { return GameManager.Instance;}
		}

	    protected int positionToGo;

		public bool IsDragged 
		{
			get { return dragObject.IsBeingDragged; }
		}

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
			validTypes.Add (TotemType.Triangle);
			validTypes.Add (TotemType.Sphere);
			validTypes.Add (TotemType.Square);
		}
		
		public void SetUp (TotemInstantiatorConfig totem, List<int> validStartPoints)
		{
			if (validTypes.Contains (totem.Type))  
			{
				this.totem = totem;
				this.validStartPoints = validStartPoints;
				GameManagerForStates.StartedGame += OnStartedGame;
				controllerToStop.CollidedWithTotem += OnCrashWithOtherCollider;
			    positionToGo = totem.PositionToGo;
			}
		}

		private void OnCrashWithOtherCollider ()
		{
		    if (GameManager.Instance.CurrentState == GameStates.Play) 
			{
                Stop();
                EndGame(name + " has been crashed with other totem");    
		    }
			
		}

		public void Stop ()
		{
			myTransform.DOKill ();
			snaper.SnapToCloserTransform ();
		}
		
		private void OnDestroy ()
		{
			if (GameManagerForStates != null) 
			{
				GameManagerForStates.StartedGame -= OnStartedGame;
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
			myTransform.DOMove (node.transform.position, speed).SetEase(Ease.Linear).OnComplete (() => GetReachedToPoint(node));
		}

		protected void GoalReachedNode(Node node)
		{
			if (node.Id == positionToGo)
				GameManagerForStates.Goal ();
			
			if(totem.SoundToGetReach != null) 
				SoundManager.Instance.Play (totem.SoundToGetReach);

			snaper.SnapInPlace ();
		}

		protected void EndGame (string warning) {
			Debug.LogWarning (warning);
			GameManagerForStates.Lose ();
		}

		public virtual void GoToSecondaryPositionToGo() 
		{
			positionToGo = totem.SecondaryPositionToGo;
			dragObject.SetCurrentNode (myTransform.position);
			Move();
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