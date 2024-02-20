using UI;

namespace Match3
{
    public interface IBoardController
    {
        void AddTile(int x, int y);
        ICoroutineProvider GetCoroutineProvider();
    }
}