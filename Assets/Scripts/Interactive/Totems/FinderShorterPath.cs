using Path;
using UnityEngine;
using System.Collections.Generic;

namespace Interactive.Detail
{
	public class FinderShorterPath 
	{
		private PathBuilderFinder pathInfo;
		private List<Connection> currentConnections = new List<Connection> ();

		public FinderShorterPath (PathBuilderFinder pathInfo)
		{
			this.pathInfo = pathInfo;
		}

		public void FindShorterPathFromTo (int fromId, int toId)
		{
			List<Connection> foundedConnections = pathInfo.Connections.FindAll (c => c.FromNode.Id == fromId);
			List<List<Connection>> paths = new List<List<Connection>> (); 
			foreach (Connection connection in foundedConnections) 
			{
				List<Connection> path = new List<Connection> ();
				path.Add (connection);
				path.AddRange (CreatePathFrom(connection, toId));
				paths.Add (path);

				if(currentConnections.Count == 0)
				{
					currentConnections.Clear ();
					currentConnections.AddRange (path);
				}
				else if(currentConnections.Count > path.Count && path.Count > 0)
				{
					currentConnections.Clear ();
					currentConnections.AddRange (path);
				}
			}
		}

		private List<Connection> CreatePathFrom (Connection connection, int toId)
		{
			List<Connection> path = new List<Connection> ();
			List<Connection> foundedConnections = pathInfo.Connections.FindAll (c => c.FromNode.Id == connection.ToNode.Id && c.ToNode.Id != connection.FromNode.Id);

			if (foundedConnections.Count == 0)
				return path;

			if (foundedConnections.Count == 1) 
			{
				path.Add (foundedConnections [0]);
				if (foundedConnections [0].ToNode.Id != toId)
					path.AddRange (CreatePathFrom (foundedConnections [0], toId));
			} 
			else 
			{

			}

			return path;
		}

		private List<Connection> AttachPath (List<Connection> foundedConnections, int toId)
		{
			List<Connection> currentPath = new List<Connection> ();
			List<List<Connection>> paths = new List<List<Connection>> (); 
			foreach (Connection connection in foundedConnections) 
			{
				List<Connection> path = new List<Connection> ();
				path.Add (connection);
				path.AddRange (CreatePathFrom (connection, toId));
				paths.Add (path);

				if(currentConnections.Count == 0)
				{
					currentPath.Clear ();
					currentPath.AddRange (path);
				}
				else if(currentPath.Count > path.Count && path.Count > 0)
				{
					currentPath.Clear ();
					currentPath.AddRange (path);
				}
			}

			return currentPath;
		}

		public void DrawLines ()
		{
			currentConnections.ForEach (c => Debug.DrawLine (c.FromNode.transform.position, c.ToNode.transform.position, Color.magenta));
		}
	}	
}