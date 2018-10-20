namespace Chess.Common
{
    public interface IGame
    {
        bool MakeMove(Player player, Field from, Field to);

        void StartGame();
    }
}