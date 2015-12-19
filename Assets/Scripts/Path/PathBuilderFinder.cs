using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Path {

	public class PathBuilderFinder
	{
		private Node[] _nodes;

		public float MaxNodeDistance 
		{
			set;
			private	get;
		}

		public PathBuilderFinder (Node[] nodes, float maxNodeDistance)
		{
			this._nodes = nodes;
			this.MaxNodeDistance = maxNodeDistance;
		}

		public Node FindNode (int positionToAdd)
		{
			foreach (Node candidateNode in _nodes) 
			{
				if (candidateNode.Id == positionToAdd)
					return candidateNode;
			}

			return null;
		}

		public List<Node> GetNodesInLongDirection (Node nodeSpnaped, int positionToGo)
		{
			List<Node> [] directions = GetDirections (nodeSpnaped, -1);
			List<Node> longList = new List<Node> ();

			foreach (List<Node> list in directions) 
			{
				if(list.Count > longList.Count)
					longList = list;
			}

			return longList;
		}

		private List<Node> [] GetDirections (Node nodeSpnaped, int positionToGo)
		{
			List<Node> nodesInForward = GetNodesInDirection (nodeSpnaped, positionToGo , Vector3.forward, new List<Node>());
			List<Node> nodesInBackward = GetNodesInDirection (nodeSpnaped, positionToGo , Vector3.back, new List<Node>());
			List<Node> nodesInLeft = GetNodesInDirection (nodeSpnaped, positionToGo , Vector3.left, new List<Node>());
			List<Node> nodesInRight = GetNodesInDirection (nodeSpnaped, positionToGo , Vector3.right, new List<Node>());
			return new [] {nodesInForward, nodesInBackward, nodesInLeft, nodesInRight};
		}

		public List<Node> GetNodes (Node nodeSpnaped, int positionToGo)
		{
			List<Node> nodes = new List<Node>();
			if (FoundNodeInDirection (nodeSpnaped, positionToGo, Vector3.forward))
			{
				nodes = GetNodesInDirection (nodeSpnaped, positionToGo, Vector3.forward, nodes);
			}
			else if (FoundNodeInDirection (nodeSpnaped, positionToGo, Vector3.back))
			{
				nodes = GetNodesInDirection (nodeSpnaped, positionToGo, Vector3.back, nodes);
			}
			else if (FoundNodeInDirection (nodeSpnaped, positionToGo, Vector3.left))
			{
				nodes = GetNodesInDirection (nodeSpnaped, positionToGo, Vector3.left, nodes);
			}
			else if (FoundNodeInDirection (nodeSpnaped, positionToGo, Vector3.right))
			{
				nodes = GetNodesInDirection (nodeSpnaped, positionToGo, Vector3.right, nodes);
			}
			return nodes;
		}

		private bool FoundNodeInDirection (Node nodeSpnaped, int positionToGo , Vector3 direction) 
		{
			Node node = GetNearsetNodeInDirection(nodeSpnaped, direction);
			
			if(node == null)
				return false;
			else if(node.Id != positionToGo)
				return FoundNodeInDirection (node, positionToGo , direction);
			else if(node.Id == positionToGo)
				return true;
			
			return false;
		}
		
		public List<Node> GetNodesInDirection (Node nodeSpnaped, int positionToGo , Vector3 direction, List<Node> newNodes)
		{
			Node node = GetNearsetNodeInDirection(nodeSpnaped, direction);

			if(node == null)
				return newNodes;

			newNodes.Add (node);

			if(node.Id != positionToGo)
				newNodes = GetNodesInDirection (node, positionToGo , direction, newNodes);
			
			return newNodes;
		}

		public Node GetNearsetNodeInDirection(Node node, Vector3 direction) 
		{
			Node nearestNode = null;
			float nearestDistance = 0.3f;
			foreach (Node candidateNode in _nodes) {
				if (IsAValidCandidateNode(node, candidateNode, direction)) {
					if (nearestNode == null || 
					    (Vector3.Distance(node.transform.position, candidateNode.transform.position) < nearestDistance)) {
						nearestNode = candidateNode;
						nearestDistance = Vector3.Distance(node.transform.position, candidateNode.transform.position);
					}
				}
			}
			//Debug.DrawLine(node.transform.position, (node.transform.position + direction), Color.magenta); 
			if (nearestNode != null) {
				Debug.DrawLine(node.transform.position, nearestNode.transform.position, Color.cyan);
				
			}
			return nearestNode;
		}

		private bool IsAValidCandidateNode(Node nodeA, Node nodeB, Vector3 direction) 
		{
            Vector3 aToB = (nodeA.transform.position - nodeB.transform.position);
			bool validDirection = (Math.Abs(Vector3.Angle(aToB, direction)) < 0.01f);
			bool validDistance = MaxNodeDistance < 0 || 
				(Vector3.Distance(nodeA.transform.position, nodeB.transform.position) < MaxNodeDistance);
			return validDirection && validDistance;
		}
	}
    
}
