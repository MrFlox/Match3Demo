using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace Match3
{
    internal class MatchChecker
    {
        public event Action<int> OnCollect;
        readonly TilesMover _tilesMover;
        readonly TileDestroyer _tileDestroyer;
        readonly Tile[,] _tiles;
        readonly IBoardController _boardController;
        public MatchChecker(Tile[,] tiles, IBoardController boardController, GameSettings gameSettings)
        {
            _tiles = tiles;
            _boardController = boardController;
            _tilesMover = new TilesMover(gameSettings, _tiles, boardController);
            _tileDestroyer = new TileDestroyer(_tiles, boardController.GetCoroutineProvider());
        }
        public void CheckMatchVertical(int posX)
        {
            var rowLength = _tiles.GetLength(1);
            var arr = GetCollArr(posX, rowLength);
            var result = MatchFinder.FindConsecutiveMatches(arr);
            if (result.Count == 0) return;
            OnCollect?.Invoke(result.Count);
            _tileDestroyer.DestroyVerticalLine(result, posX, ()=> _tilesMover.MoveVerticalLine(posX, result));
        }
        public void CheckMatchHorizontal(int rowIndex)
        {
            var rowLength = _tiles.GetLength(0);
            var arr = GetRowArr(rowIndex, rowLength);
            var result = MatchFinder.FindConsecutiveMatches(arr);
            if (result.Count == 0) return;
            OnCollect?.Invoke(result.Count);
            _tileDestroyer.DestroyHorizontalLine(result, rowIndex);
            _tilesMover.MoveHorizontal(result, rowIndex, () => AddNewTiles(result));
        }
        int[] GetCollArr(int colIndex, int rowLength)
        {
            Debug.Log($"GetCollArr {colIndex}; {rowLength}");
            var arr = new int[rowLength];
            for (var i = 0; i < rowLength; i++)
                arr[i] = _tiles[colIndex, i].CurrentColorIndex;
            return arr;
        }
        int[] GetRowArr(int rowIndex, int rowLength)
        {
            var arr = new int[rowLength];
            for (var i = 0; i < rowLength; i++)
                arr[i] = _tiles[i, rowIndex].CurrentColorIndex;
            return arr;
        }
        void AddNewTiles(List<int> result)
        {
            foreach (var col in result)
                _boardController.AddTile(col, 0);
        }
    }
}