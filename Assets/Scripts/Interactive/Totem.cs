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
		private List<int> validStartPoints;
		private DraggableObject dragObject;
		private IGameManagerForStates gameStates;
		private TotemControllerStop controllerToStop;
		private SnapItemToCloserPosition snaper;

		protected Transform myTransform;
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
			if (totem.Type == TotemType.Single) 
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

			snaper.SnapInPlace ();
		}

		protected void EndGame (string warning)
		{
			Debug.LogWarning (warning);
			gameStates.Lose ();
		}

		
		#if UNITY_EDITOR
		protected void Update ()
		{
			Vector3 position = myTransform.position;
			Debug.DrawLine(position , position - (Vector3.forward * 0.1F), Color.red);
			Debug.DrawLine(position , position - (Vector3.back * 0.1F), Color.blue);
			Debug.DrawLine(position , position - (Vector3.left * 0.1F), Color.yellow);
			Debug.DrawLine(position , position - (Vector3.right * 0.1F), Color.green);
		}
		#endif

		protected abstract void GetReachedToPoint (Node node);
		protected abstract void Move ();
	}
}