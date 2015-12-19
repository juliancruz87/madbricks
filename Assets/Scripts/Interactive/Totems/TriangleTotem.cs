using Path;
using UnityEngine;
using System.Collections.Generic;
using ManagerInput.CameraControls;
using ManagerInput;
using DG.Tweening;

namespace Interactive.Detail
{
	public class TriangleTotem : Totem
	{
		private Node cachedNode;
		private bool canRotate = false;
		private Collider myCollider;
		private Vector3 lastDirection;

		private bool CanRotate 
		{
			get{ return TouchChecker.InputIsOverThisCollider(Camera.main, myCollider) && InputManager.Instance.InputDevice.PrimaryTouch.ReleasedTapThisFrame && canRotate;}
		}

		protected override void Awake ()
		{
			base.Awake ();
			myCollider = GetComponent<Collider> ();
		}

		protected override void Move ()
		{
			List<Node> nodes = new List<Node> ();
			nodes = Finder.GetNodesInDirection (CurrentNode, totem.PositionToGo, myTransform.forward, nodes);
			if (nodes.Count > 0)
				ChoseNodeToGo (nodes);
			else 
				Debug.LogWarning (gameObject.name + " wasn't found a path to follow");
		}

		private void ChoseNodeToGo (List<Node> nodes)
		{
			Node node = nodes [nodes.Count - 1];
			float speed = totem.SpeedPerTile * nodes.Count;
			GoToNode (node, speed);
		}

		protected override void GetReachedToPoint (Node node)
		{
			if (node.Id == totem.PositionToGo) 
				GoalReachedNode (node);
			else 
				GoToOtherNode ();
		}

		private float GetCloserDegrees (float currentDegrees)
		{
			float closerDegress = NumberHelper.GetCloserFloatInList (360, currentDegrees , new List<float>{0f,90f,180f,270f,360f});
			return closerDegress == 360f ? 0f : closerDegress; 
		}

		protected override void Update ()
		{
			base.Update ();
			if (cachedNode != CurrentNode) 
			{
				cachedNode = CurrentNode;
				canRotate = IsInStartPoint;
			}

			if(CanRotate)
				myTransform.DORotate ((myTransform.rotation.eulerAngles.y - 90) * Vector3.up, 0.3F);

		}

		private void GoToOtherNode ()
		{
			Node leftNode = Finder.GetNearsetNodeInDirection (CurrentNode, myTransform.right * -1);
			Node rightNode = Finder.GetNearsetNodeInDirection (CurrentNode, myTransform.right);
			MoveToNextNode (leftNode, rightNode, myTransform.rotation.eulerAngles, (Vector3.up * 90f));
		}

		private void MoveToNextNode (Node leftNode, Node rightNode, Vector3 currentEulers, Vector3 turn90Dregrees)
		{
			if (leftNode != null)
				myTransform.DORotate (currentEulers - turn90Dregrees, 0.3F).OnComplete (() => Move ());
			else if (rightNode != null)
				myTransform.DORotate (currentEulers + turn90Dregrees, 0.3F).OnComplete (() => Move ());
		}
	}
}