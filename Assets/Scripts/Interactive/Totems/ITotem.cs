namespace Interactive
{
	public interface ITotem
	{
		bool IsDragged { get;}
		IGameManagerForStates GameStates { set; }
	}
}