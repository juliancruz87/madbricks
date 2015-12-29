using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Path
{
    public class DijkstraPathFinder {

        public static List<Node> FindShortestPath(Node start, Node end) {
            NodeRecord startRecord = new NodeRecord();
            startRecord = new NodeRecord();
            startRecord.Node = start;
            startRecord.Connection = null;
            startRecord.CostSoFar = 0;

            ArrayList open = new ArrayList();
            open.Add(startRecord);
            ArrayList closed = new ArrayList();

            NodeRecord current = null;

            while (open.Count > 0) {
                current = NodeRecord.SmallestElement(open);
                if (current.Node == end)
                    break;

                ArrayList connections = current.Node.Connections;

                foreach (Connection connection in connections) {
                    Node endNode = connection.ToNode;
                    float endNodeCost = current.CostSoFar + connection.Weight;
                    NodeRecord endNodeRecord;
                    if (NodeRecord.Contains(closed, endNode))
                        continue;

                    if (NodeRecord.Contains(open, endNode))
                    {
                        endNodeRecord = NodeRecord.Find(open, endNode);
                        if (endNodeRecord.CostSoFar <= endNodeCost)
                            continue;
                    }
                    else {
                        endNodeRecord = new NodeRecord();
                        endNodeRecord.Node = endNode;
                    }

                    endNodeRecord.CostSoFar = endNodeCost;
                    endNodeRecord.Connection = connection;
                    endNodeRecord.Node.pathConnection = connection;
                    endNodeRecord.Connection.FromNodeRecord = current;
                    endNodeRecord.Connection.ToNodeRecord = endNodeRecord;

                    if (!NodeRecord.Contains(open, endNode)) {
                        open.Add(endNodeRecord);
                    }
                }

                open.Remove(current);
                closed.Add(current);
            }

            if (current == null || current.Node != end) {
                Debug.Log("The algorithm couldnt find a path");
                return null;
            }
                
            
            //Debug.Log("Here creating the reverse path");
            ArrayList path = new ArrayList();
            while (current.Node != start) {
                path.Add(current.Node);
                //Debug.Log("path node: " + current.Node.Id); 
                current = current.Connection.FromNodeRecord;
            }
            
            return InvertPath(path);
        }

        private static List<Node> InvertPath(ArrayList path) {
            List<Node> invertedPath = new List<Node>();
            for (int i = path.Count - 1; i >= 0; i--)
                invertedPath.Add((Node)path[i]);

            //foreach (Node node in invertedPath)
             //   Debug.Log("path inverted node: " + node.Id);

            return invertedPath;
        }
    }
}