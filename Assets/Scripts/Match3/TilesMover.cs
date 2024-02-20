using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using ScriptableObjects;
using UI;
using UnityEngine;

namespace Match3
{
    internal class TilesMover : ICoroutineProvider
    {
        const float AnimationTime =.5f;
        readonly ICoroutineProvider _corProvider;
        readonly GameSettings _settings;
        readonly Tile[,] _tiles;
        readonly IBoardController _boardController;
        public TilesMover(GameSettings settings, Tile[,] tiles, IBoardController boardController)
        {
            _corProvider = boardController.GetCoroutineProvider();
            _settings = settings;
            _tiles = tiles;
            _boardController = boardController;
        }
        public Coroutine StartCoroutine(IEnumerator routine) => _corProvider.StartCoroutine(routine);

        public void MoveVerticalLine(int posX, List<int> result)
        {
            StartCoroutine(
                MoveVertLineAfterDelete(posX, result.Count, result.Min()));
        }
        public void MoveHorizontal(List<int> result, int rowIndex, Action action)
        {
            StartCoroutine(MoveAfterDelay(result, rowIndex, action));
        }
        void MoveTile(Tile tile, int steps = 1)
        {
            StartCoroutine(MoveTileWithAnimation(tile, steps));
        }
        IEnumerator MoveVertLineAfterDelete(int colIndex, int steps, int minRowIndex)
        {
            var tilesToMove = new List<Tile>();
            for (var i = minRowIndex - 1; i >= 0; i--)
                tilesToMove.Add(_tiles[colIndex, i]);

            foreach (var tile in tilesToMove)
                MoveTile(tile, steps);

            yield return new WaitForSeconds(AnimationTime);
            for (var y = 0; y < steps; y++)
                _boardController.AddTile(colIndex, y);
        }
        IEnumerator MoveAfterDelay(List<int> result, int rowIndex, Action onComplete)
        {
            yield return new WaitForSeconds(.2f);
            MoveCols(result, rowIndex);
            onComplete?.Invoke();
        }
        void MoveCols(List<int> colsToMove, int rowIndex)
        {
            foreach (var col in colsToMove)
            {
                var tilesToMove = GetTileToMove(col, rowIndex);
                MoveTiles(tilesToMove);
            }
        }
        List<Tile> GetTileToMove(int colsToMove, int rowIndex)
        {
            var result = new List<Tile>();
            for (var i = rowIndex - 1; i >= 0; i--)
                result.Add(_tiles[colsToMove, i]);
            return result;
        }
        void MoveTiles(List<Tile> tilesToMove)
        {
            foreach (var tile in tilesToMove)
                MoveTile(tile);
        }
        IEnumerator MoveTileWithAnimation(Tile tile, int steps = 1)
        {
            var tileTransform = tile.GetComponent<RectTransform>();
            var newPos = GetNewPos(steps, tileTransform);
            tile.SetTileOnGrid(GetNewGridPos(tile, steps));

            var startTime = Time.time;
            while (Time.time - startTime < AnimationTime)
            {
                var delta = (Time.time - startTime) / AnimationTime;
                tileTransform.anchoredPosition = Vector2.Lerp(
                    tileTransform.anchoredPosition,
                    newPos, delta);
                yield return null;
            }
            tileTransform.anchoredPosition = newPos;
        }
        Vector2 GetNewPos(int steps, RectTransform tileTransform)
        {
            var oldPos = tileTransform.anchoredPosition;
            oldPos.y -= _settings.tileSize * steps;
            return oldPos;
        }
        static Vec2 GetNewGridPos(Tile tile, int steps)
        {
            var tmpVec = tile.gridPosition;
            tmpVec.Y += steps;
            return tmpVec;
        }
    }
}