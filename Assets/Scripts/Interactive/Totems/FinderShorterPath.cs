using Path;
using UnityEngine;
using System.Collections.Generic;

namespace Interactive.Detail
{
	public class FinderShorterPath 
	{
		private int reachPoint = 0;
		private PathBuilderFinder pathInfo;
		private List<Connection> shorterPath = new List<Connection> ();
		private List<Connection> markedConnections = new List<Connection> ();

		public List<Node> FindShorterPathFromTo (int fromId, int toId, PathBuilderFinder pathInfo)
		{
            Debug.Log("FindShorterPathFromTo");
			reachPoint = toId;
			this.pathInfo = pathInfo;

			List<Connection> foundedConnections = pathInfo.Connections.FindAll (c => c.FromNode.Id == fromId);
			List<List<Connection>> paths = CreatePathsConnections (foundedConnections, markedConnections, true);
			shorterPath = SelectShorterPath (paths);

			return GetNodesInPath (shorterPath);
		}

		private List<Connection> AddAlternativePaths (List<Connection> foundedConnections, List<Connection> markedConnections)
		{
			List<List<Connection>> paths = CreatePathsConnections (foundedConnections, markedConnections, false);
			return SelectShorterPath (paths);
		}

		private List<List<Connection>> CreatePathsConnections (List<Connection> foundedConnections, List<Connection> markedConnections, bool isStartPoint)
		{
			List<List<Connection>> paths = new List<List<Connection>> (); 
			foreach (Connection connection in foundedConnections)
			{
				if(isStartPoint)
					markedConnections = new List<Connection> ();

				List<Connection> path = CreatePath (connection, markedConnections);
				if(path.FindAll (c=> c.ToNode.Id == reachPoint).Count > 0)
					paths.Add (path);
			}
			return paths;
		}

		private List<Connection> CreatePath (Connection connection, List<Connection> markedConnections)
		{
			List<Connection> path = new List<Connection> ();
			if (!path.Contains (connection) && !markedConnections.Contains (connection)) 
			{
				markedConnections.Add (connection);
				path.Add (connection);
			}

			if (connection.ToNode.Id != reachPoint) 
			{
				List<Connection> alternativePaths = pathInfo.Connections.FindAll (c => c.FromNode.Id == connection.ToNode.Id && c.ToNode.Id != connection.FromNode.Id && !markedConnections.Contains (c));
				bool hasAlternativePaths = alternativePaths.Count > 0;
				if (hasAlternativePaths)
					path.AddRange (AddAlternativePaths (alternativePaths, markedConnections));
			}

			return path;
		}

		private List<Connection> SelectShorterPath (List<List<Connection>> paths)
		{
			List<Connection> selectedPath = new List<Connection> ();

			foreach (List<Connection> path in paths) 
			{
				if (selectedPath.Count == 0)
					selectedPath = path;
				else if (selectedPath.Count > path.Count)
					selectedPath = path;
			}

			return selectedPath;
		}

		private List<Node> GetNodesInPath (List<Connection> shorterPath)
		{
			List<Node> nodes = new List<Node> ();
			foreach (Connection connection in shorterPath)
			{
				if(!nodes.Contains(connection.FromNode))
					nodes.Add (connection.FromNode);

				if(!nodes.Contains(connection.ToNode))
					nodes.Add (connection.ToNode);
			}

			return nodes;
		}

		public void DrawShorterParthDebug ()
		{
			shorterPath.ForEach (c => Debug.DrawLine (c.FromNode.transform.position, c.ToNode.transform.position, Color.magenta));
		}
	}	
}