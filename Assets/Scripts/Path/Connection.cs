namespace Path {

    public class Connection {

        public Node FromNode { get; set; }
        public Node ToNode { get; set; }
        public NodeRecord FromNodeRecord { get; set; }
        public NodeRecord ToNodeRecord { get; set; }
        public float Weight { get; set; }

        public Connection(Node fromNode, Node toNode, float weight) {
            FromNode = fromNode;
            ToNode = toNode;
            Weight = weight;
        }

		public bool Contains (Node node)
		{
			return FromNode == node || ToNode == node;
		}
    }
}
