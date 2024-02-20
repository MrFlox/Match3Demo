using System.Collections;
using Core;
using ScriptableObjects;
using SharedComponents;
using UnityEngine;

namespace Match3
{
    public class GameBoard : IService
    {
        readonly IBoardController _boardController;
        public readonly Tile[,] Tiles;
        readonly int _rows;
        readonly int _cols;
        readonly IGameScore _gameScore;
        readonly MatchChecker _matchChecker;

        public GameBoard(IBoardController boardController)
        {
            _boardController = boardController;
            var gameSettings = Locator.Get<GameSettings>();
            _gameScore = Locator.Get<IGameScore>();
            _rows = gameSettings.BoardSize;
            _cols = gameSettings.BoardSize;
            Tiles = new Tile[_rows, _cols];
            _matchChecker = new MatchChecker(Tiles, boardController, gameSettings);
            _matchChecker.OnCollect += OnCollect;
        }
        public void AddTile(Tile tile)
        {
            Tiles[tile.gridPosition.X, tile.gridPosition.Y] = tile;
            tile.UpdateName();
        }
        public void ChangeNeighbours(SwipeDirection direction, Tile tile)
        {
            var pos = tile.gridPosition.GetGridSwipePosition(direction);
            if (IsOutOfBounds(pos)) return;
            var neighbour = Tiles[pos.X, pos.Y];
            if (neighbour == null) return;
            tile.SwapWithTile(neighbour, pos);
            CheckMatchesWithDelay();
        }
        void CheckMatchesWithDelay()
        {
            _boardController.GetCoroutineProvider().StartCoroutine(CheckMatches());
        }
        IEnumerator CheckMatches()
        {
            float timeDelay = .1f;
            for (var y = 0; y < _rows; y++)
            {
                yield return new WaitForSeconds(timeDelay);
                _matchChecker.CheckMatchHorizontal(y);
            }

            for (var x = 0; x < _cols; x++)
            {
                yield return new WaitForSeconds(timeDelay);
                _matchChecker.CheckMatchVertical(x);
            }
            yield return null;
        }
        void OnCollect(int collectedCount) => CollectScores(collectedCount);
        bool IsOutOfBounds(Vec2 pos) => pos.X < 0 || pos.X >= _cols + 1 || pos.Y < 0 || pos.Y >= _rows + 1;
        void CollectScores(int resultCount) => _gameScore.AddScore(resultCount);
    }
}