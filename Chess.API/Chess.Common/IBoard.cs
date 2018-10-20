namespace Chess.Common
{
    public interface IBoard
    {
        bool MakeMove(Player player, Field from, Field to);
    }
}