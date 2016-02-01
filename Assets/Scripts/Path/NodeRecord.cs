using System.Collections;

namespace Path {
    public class NodeRecord {
        public Node Node { get; set; }
        public Connection Connection { get; set; }
        public float CostSoFar { get; set; }
        
        public static NodeRecord SmallestElement(ArrayList nodeRecords) {
            NodeRecord smallestElement = null;

            foreach (NodeRecord nodeRecord in nodeRecords)
            {
                if (smallestElement == null)
                    smallestElement = nodeRecord;

                else if (nodeRecord.CostSoFar <= smallestElement.CostSoFar)
                    smallestElement = nodeRecord;
            }

            return smallestElement;
        }

        public static bool Contains(ArrayList nodeRecords, Node node) {
            foreach (NodeRecord nodeRecord in nodeRecords) {
                if (nodeRecord.Node == node)
                    return true;
            }
            return false;
        }

        public static NodeRecord Find(ArrayList nodeRecords, Node node) {
            foreach (NodeRecord nodeRecord in nodeRecords) {
                if (nodeRecord.Node == node)
                    return nodeRecord;
            }
            return null;
        }
    }
}