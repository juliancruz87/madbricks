using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UI;
using Path;

namespace Interactive.Detail
{
	public class ShowExtraPathTutorialStep : ShowPathTutorialStep
	{
        [SerializeField]
        private int initialPosition;

        [SerializeField]
        private int finalPosition;

        protected override void GetPositions()
        {
            PathBuilder Path = PathBuilder.Instance;
            Node initialNode = Path.GetNodeById(initialPosition);

            List<Node> nodes = Path.Finder.GetNodes(initialNode, finalPosition, initialNode.transform);
            List<Vector3> nodePositions = nodes.ConvertAll(item => item.transform.position);
            nodePositions.Insert(0, initialNode.transform.position);
            positions = nodePositions.ToArray();
        }

    }
}