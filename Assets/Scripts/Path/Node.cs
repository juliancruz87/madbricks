using UnityEngine;

namespace Path {
    public class Node : MonoBehaviour {
		[SerializeField]
		private int id;

        [SerializeField]
        // (0,0,1)
        private Node upNode;

        [SerializeField]
        // (0,0,-1)
        private Node downNode;

        [SerializeField]
        // (-1,0,0)
        private Node leftNode;

        [SerializeField]
        // (1,0,0)
        private Node rightNode;

        public Node UpNode {
            get { return upNode; }
            set { upNode = value; }
        }

        public Node DownNode {
            get { return downNode; }
            set { downNode = value; }
        }

        public Node LeftNode {
            get { return leftNode; }
            set { leftNode = value; }
        }

        public Node RightNode {
            get { return rightNode; }
            set { rightNode = value; }
        }

        public int Id {
			get { return id; }
		}

		public void SetUp (int id) {
			this.id = id;
		}
    }
}
