using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Path {

    public class PathBuilder : MonoBehaviour {

        private Node[] _nodes;
        private ArrayList _connections;

        public float maxNodeDistance = -1;

        public bool debugModeOn;
        public Vector3 debugOffset;
        public Color debugColor;
        // Use this for initialization

		public static PathBuilder Instance
		{
			get;
			private set;
		}

		private void Awake ()
		{
			if(Instance == null)
				Instance = this;
		}

        private void Start () {
            _nodes = FindObjectsOfType<Node>();
            BuildConnections();
        }

		public List<Connection> GetShortPath (Node nodeSpnaped, int positionToGo) {
			/*Node nodeToGo = System.Array.Find (_nodes,c=> c.Id == positionToGo);
			List<Connection> possiblePaths = GetConnectionsByNode (nodeSpnaped);

			List<PossiblePath> paths = new List<PossiblePath>();
			for (int i = 0; i < possiblePaths.Count; i++ )
			{
				paths[i].AddNode (nodeSpnaped);
				paths[i].AddNode (possiblePaths[i].ToNode);
			}*/

			return new List<Connection> ();
		}

		public List<Connection> GetConnectionsByNode (Node node)
		{
			List<Connection> connections = new List<Connection> ();

			foreach (Connection connection in _connections)
			{
				if( connection.FromNode == node)
					connections.Add (connection);
			}

			return connections;
		}

        private void BuildConnections() {
            _connections =  new ArrayList();
            foreach (Node node in _nodes) {
                AddConnectionInDirection(node, new Vector3(-1, 0, 0));
                AddConnectionInDirection(node, new Vector3(1, 0, 0));
                AddConnectionInDirection(node, new Vector3(0, 0, -1));
                AddConnectionInDirection(node, new Vector3(0, 0, 1));
            }
        }

        private void AddConnectionInDirection(Node node, Vector3 direcion) {
            Node nearestNode = GetNearsetNodeInDirection(node, direcion);
            if (nearestNode != null)
                _connections.Add(new Connection(node, nearestNode, 1));
        }

        public Node GetNearsetNodeInDirection(Node node, Vector3 direction) {
            Node nearestNode = null;
            float nearestDistance = 0;
            foreach (Node candidateNode in _nodes) {
                if (IsAValidCandidateNode(node, candidateNode, direction)) {
                    if (nearestNode == null || 
                        (Vector3.Distance(node.transform.position, candidateNode.transform.position) < nearestDistance)) {
                            nearestNode = candidateNode;
                            nearestDistance = Vector3.Distance(node.transform.position, candidateNode.transform.position);
                        }
                }
            }
            Debug.DrawLine(node.transform.position, (node.transform.position + direction), Color.magenta); 
            if (nearestNode != null) {
                Debug.DrawLine(node.transform.position, nearestNode.transform.position, Color.cyan);
                
            }
            return nearestNode;
        }

        private bool IsAValidCandidateNode(Node nodeA, Node nodeB, Vector3 direction) {
            Vector3 aToB = (nodeA.transform.position - nodeB.transform.position);
            bool validDirection = (Math.Abs(Vector3.Angle(aToB, direction)) < 0.01f);
            bool validDistance = maxNodeDistance < 0 || 
                                    (Vector3.Distance(nodeA.transform.position, nodeB.transform.position) < maxNodeDistance);
            return validDirection && validDistance;
        }

        // Update is called once per frame
        void Update () {
            if (debugModeOn)
                DrawDebugLines();
        }

        private void DrawDebugLines() {
            foreach (Connection connection in _connections) {
                Vector3 startPosition = connection.FromNode.transform.position;
                startPosition += debugOffset;
                Vector3 endPosition = connection.ToNode.transform.position;
                endPosition += debugOffset;
                Debug.DrawLine(startPosition, endPosition, debugColor);
            }
                
        }


        public Node GetNearsetNode(Vector3 position) { 
            Node nearestNode = null;
            float nearestDistance = 0;
            foreach (Node candidateNode in _nodes) {
                if (nearestNode == null ||
                    (Vector3.Distance(position, candidateNode.transform.position) < nearestDistance)) {
                    nearestNode = candidateNode;
                    nearestDistance = Vector3.Distance(position, candidateNode.transform.position);
                }
            }
            return nearestNode;
        }
    }
}
