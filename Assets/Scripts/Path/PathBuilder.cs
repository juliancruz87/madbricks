using System;
using System.Collections;
using UnityEngine;

namespace Path {

    public class PathBuilder : MonoBehaviour {

        private Node[] _nodes;
        private ArrayList _connections;

        public float maxNodeDistance = -1;

        public bool debugModeOn;
        public Vector3 debugOffset;
        public Color debugColor;
        // Use this for initialization
        void Start () {
            _nodes = FindObjectsOfType<Node>();
            BuildConnections();
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

        private Node GetNearsetNodeInDirection(Node node, Vector3 direction) {
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
    }
}
