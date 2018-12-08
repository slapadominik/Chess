using System;

namespace Chess.Logic
{
    public class Player
    {
        public Guid Id { get; }
        public Color Color { get; }
        public string Name { get; set; }

        public Player(Guid id, Color color)
        {
            Id = id;
            Color = color;
        }

        public override bool Equals(object obj)
        {
            if (Equals(obj))
            {
                return true;
            }

            if (obj is Player)
            {
                return Id == (obj as Player).Id;
            }

            return false;
        }
    }
}