using System;

namespace SharedComponents
{
    public interface ISwiper
    {
        event Action<SwipeDirection> OnSwipe;
    }
}