using Path;
using Drag;
using UnityEngine;

namespace Interactive
{
	public interface ITotem
	{
		bool IsDragged { get;}
		bool IsBoss { get;}
		bool IsJailed { get; }
        bool IsInStartPoint { get; }

		Node CurrentNode { get; }
		DraggableObject DragObject { get;}

        Vector3[] GetPathPositions();
        void SetHighlight(bool active);
	}
}