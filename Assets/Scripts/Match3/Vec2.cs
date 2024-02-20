using System;
using SharedComponents;

namespace Match3
{
    [Serializable]
    public struct Vec2
    {
        public int X;
        public int Y;
        public Vec2(int nX, int nY)
        {
            X = nX;
            Y = nY;
        }
        public Vec2 GetGridSwipePosition(SwipeDirection direction)
         {
             var pos = this;
            switch (direction)
            {
                case SwipeDirection.Left:
                    pos.X--;
                    break;
                case SwipeDirection.Right:
                    pos.X++;
                    break;
                case SwipeDirection.Top:
                    pos.Y--;
                    break;
                case SwipeDirection.Bottom:
                    pos.Y++;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, null);
            }
            return pos;
        }
    }
}