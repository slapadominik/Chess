namespace Chess.Logic
{
    public enum MoveStatus
    {
        Normal = 1,
        Capture = 2,
        PawnPromotion = 3,
        Check = 4,
        Checkmate = 5
    }
}