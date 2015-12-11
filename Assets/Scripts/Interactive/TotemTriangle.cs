using Path;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using ManagerInput.CameraControls;
using ManagerInput;

namespace Interactive.Detail
{
	public class TotemTriangle : Totem
	{
		private Dictionary<float,Vector3> directionByRotation = new Dictionary<float, Vector3> ();
		private Node cachedNode;
		private bool canRotate = false;
		private Collider myCollider;

		protected override void Awake ()
		{
			base.Awake ();
			directionByRotation.Add (0, Vector3.forward);
			directionByRotation.Add (90, Vector3.left);
			directionByRotation.Add (180, Vector3.back);
			directionByRotation.Add (270, Vector3.right);
			directionByRotation.Add (360, Vector3.forward);
			myCollider = GetComponent<Collider> ();
		}

		protected override void Move ()
		{
			List<Node> nodes = GetNodesToTravel ();
			if (nodes.Count > 0) 
			{
				Node node = nodes [nodes.Count - 1];
				float speed = totem.SpeedPerTile * nodes.Count;
				GoToNode (node, speed);
			} 
			else 
			{
				Debug.LogWarning (gameObject.name + " wasn't found a path to follow");
			}
		}

		private List<Node> GetNodesToTravel ()
		{
			List<Node> nodes = new List<Node> ();
			float currentDegrees = GetCloserDegrees (myTransform.localRotation.eulerAngles.y);
			Vector3 directionToGo = directionByRotation[currentDegrees];
			nodes = PathBuilder.Instance.Finder.GetNodesInLongDirection (CurrentNode, totem.PositionToGo);
			return nodes;
		}

		protected override void GetReachedToPoint (Node node)
		{
			if (node.Id == totem.PositionToGo) 
				GoalReachedNode (node);
			else 
				GoToOtherPoint ();
		}

		private float GetCloserDegrees (float currentDegrees)
		{
			float closerDegress = NumberHelper.GetCloserFloatInList (360, currentDegrees , new List<float>{0f,90f,180f,270f,360f});
			return closerDegress == 360f ? 0f : closerDegress; 
		}

		protected override void Update ()
		{
			if (cachedNode != CurrentNode) 
			{
				cachedNode = CurrentNode;
				canRotate = IsInStartPoint;
			}

			if(TouchChecker.InputIsOverThisCollider(Camera.main, myCollider) && canRotate && InputManager.Instance.InputDevice.PrimaryTouch.ReleasedTapThisFrame)
				myTransform.DORotate ((myTransform.rotation.eulerAngles.y - 90) * Vector3.up, 0.3F);

		}

		private void GoToOtherPoint ()
		{
			Vector3 currentEulers = myTransform.rotation.eulerAngles;
			Vector3 turn90Dregrees = Vector3.up * 90f;

			if (Random.Range (0, 100) < 50)
				myTransform.DORotate (currentEulers - turn90Dregrees, 0.3F);
			else
				myTransform.DORotate (currentEulers - turn90Dregrees, 0.3F);

			Move ();
		}
	}
}