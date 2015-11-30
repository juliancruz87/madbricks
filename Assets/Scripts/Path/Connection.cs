namespace Path {

    public class Connection {

        public Node FromNode { get; set; }
        public Node ToNode { get; set; }
        public float Weight { get; set; }

        public Connection(Node fromNode, Node toNode, float weight) {
            FromNode = fromNode;
            ToNode = toNode;
            Weight = weight;
        }
    }
}
