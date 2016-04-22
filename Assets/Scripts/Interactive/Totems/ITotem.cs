using Path;
using Drag;

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

		void SetHighlight(bool active);
	}
}