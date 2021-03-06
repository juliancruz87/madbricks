﻿using System;
using System.Collections;
using UnityEngine;
using System.Collections.Generic;

namespace Path {
	
    public class PathBuilder : MonoBehaviour {
		public float maxNodeDistance = -1;

        private Node[] _nodes;
        private ArrayList _connections;

        public bool debugModeOn;
        public Vector3 debugOffset;
        public Color debugColor;
		private PathBuilderFinder finder;

		public static PathBuilder Instance
		{
			get;
			private set;
		}

		public PathBuilderFinder Finder 
		{
			get { return finder; }
		}

		private void Awake ()
		{
			if(Instance == null)
				Instance = this;
		}

        private void Start () {
            _nodes = FindObjectsOfType<Node>();
            finder = new PathBuilderFinder (_nodes, maxNodeDistance);
            BuildConnections();
			finder.SetConnections (_connections);
        }
	
        private void BuildConnections() {
            _connections =  new ArrayList();
            foreach (Node node in _nodes) {
                node.LeftNode = AddConnectionInDirection(node, new Vector3(-1, 0, 0));
                node.RightNode = AddConnectionInDirection(node, new Vector3(1, 0, 0));
                node.DownNode = AddConnectionInDirection(node, new Vector3(0, 0, -1));
                node.UpNode = AddConnectionInDirection(node, new Vector3(0, 0, 1));
            }
        }

        private Node AddConnectionInDirection(Node node, Vector3 direcion) {
			Node nearestNode = finder.GetNearsetNodeInDirection(node, direcion);
            if (nearestNode != null) {
                Connection newConnection = new Connection(node, nearestNode, 1);
                _connections.Add(newConnection);
                node.AddConnection(newConnection);
            }
            return nearestNode;
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

        public Node GetNodeById(int id) {
            foreach (Node node in _nodes) {
                if (node.Id == id) {
                    return node;
                }
            }

            return null;
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
