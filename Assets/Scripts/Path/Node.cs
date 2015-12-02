using UnityEngine;

namespace Path {
    public class Node : MonoBehaviour {
		[SerializeField]
		private int id;

		public int Id 
		{
			get { return id; }
		}
    }
}
