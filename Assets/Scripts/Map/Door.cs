using UnityEngine;
using DG.Tweening;

namespace Map 
{
	public enum State
	{
		None = 0,
		Open = 1,
		Close = 2,
	}

	public class Door : MonoBehaviour 
	{
		[SerializeField]
		private State state;

		[SerializeField]
		private float closePos;

		[SerializeField]
		private float openPos;

		[SerializeField]
		private float speed;

		private Renderer myRenderer;
		private Collider myCollider;
		private Transform myTransform;

		public State State 
		{
			get { return state; }
		}

		private void Awake ()
		{
			myRenderer = GetComponent<Renderer> ();
			myCollider = GetComponent<Collider> ();
			myTransform = GetComponent<Transform> ();

			SetInitState ();
		}

		private void SetInitState ()
		{
			if (state == State.Close)
				Close ();
			else if (state == State.Open)
				Open ();
		}

		public void Close ()
		{
			myTransform.DOKill ();
			myTransform.DOLocalMoveY (closePos, speed);
			myCollider.enabled = true;
			myRenderer.enabled = true;
		}

		public void Open ()
		{
			myTransform.DOKill ();
			myTransform.DOLocalMoveY (openPos, speed);
			myCollider.enabled = false;
			myRenderer.enabled = false;
		}

		public void ToggleState ()
		{
			if (state == State.Close)
				Open ();
			else if (state == State.Open)
				Close ();
		}
	}
}