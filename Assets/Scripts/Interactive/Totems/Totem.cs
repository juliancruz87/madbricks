using System;
using Path;
using Drag;
using Sound;
using UnityEngine;
using Interactive;
using DG.Tweening;
using System.Collections.Generic;
using Graphics;

namespace Interactive.Detail
{
	public abstract class Totem : MonoBehaviour, ITotem
	{
		private DraggableObject dragObject;
		private TotemControllerStop controllerToStop;
		private SnapItemToCloserPosition snaper;
		private List<int> validStartPoints;
		private List<TotemType> validTypes = new List<TotemType> ();
		private HighlightObject highlightObject;

        protected Animator myAnimator;
		protected Transform myTransform;
		protected TotemInstantiatorConfig totem;
		protected int positionToGo;

		public abstract TotemType Type { get; }

		protected IGameManagerForStates GameManagerForStates
		{
			get { return GameManager.Instance;}
		}

		public bool IsDragged 
		{
			get { return dragObject.IsBeingDragged; }
		}
			
		public bool IsBoss 
		{
			get { return false; }
		}

		public bool IsJailed 
		{
			get { return false; }
		}

	    public bool IsInStartPoint
		{
			get { return dragObject.CurrentNode != null && validStartPoints != null && validStartPoints.Contains (CurrentNode.Id); }
		}

		public DraggableObject DragObject
		{
			get {  return dragObject; }
		}

		protected PathBuilderFinder Finder 
		{
			get { return PathBuilder.Instance.Finder; }
		}

		public Node CurrentNode 
		{
			get{ return dragObject.CurrentNode;}
		}

		protected virtual void Awake ()
		{
			myTransform = transform;
			snaper = GetComponent<SnapItemToCloserPosition> ();
			dragObject = GetComponent<DraggableObject> ();
			controllerToStop = GetComponent<TotemControllerStop> ();
			highlightObject = GetComponent<HighlightObject> ();
            myAnimator = GetComponentInChildren<Animator>();
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

		private void OnCrashWithOtherCollider (GameObject collidedTotem)
		{
		    if (GameManager.Instance.CurrentState == GameStates.Play) 
			{
                myAnimator.SetTrigger("Explode");
                Stop();
                EndGame(name + " has been crashed with other totem");    
		    }
			
		}

        private void TagErrorTotem(GameObject totem)
        {
            gameObject.tag = "TotemError";
        }

		public void Stop ()
		{
			myTransform.DOKill ();
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
		}

		protected void GoToNode (Node node, float speed)
		{
			myTransform.DOMove (node.transform.position, speed).SetEase(Ease.Linear).OnComplete (() => GetReachedToPoint(node));
		}

		protected void GoalReachedNode(Node node)
		{
            if (node.Id == positionToGo)
            {
                myAnimator.SetTrigger("Land");
                GameManagerForStates.Goal();
            }
			
			if(totem.SoundToGetReach != null) 
				SoundManager.Instance.Play (totem.SoundToGetReach);

		}

		protected void EndGame (string warning)
        {
            TagErrorTotem(gameObject);
            Debug.LogWarning (warning);
			GameManagerForStates.Lose ();
		}

		public virtual void GoToSecondaryPositionToGo() 
		{
			positionToGo = totem.SecondaryPositionToGo;
			dragObject.SetCurrentNode (myTransform.position);
			Move();
		}

		public void SetHighlight (bool IsActive)
		{
			if (highlightObject != null)
			{
				if (IsActive)
					highlightObject.ActivateHighlight ();
				else
					highlightObject.DeactivateHighlight ();
			}
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

        public abstract Vector3[] GetPathPositions();
		protected abstract void GetReachedToPoint (Node node);
		protected abstract void Move ();
	}
}