using System;
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Match3
{
    public class TileDestroyer
    {
        const float AnimationTime = .5f;
        readonly Tile[,] _tiles;
        readonly ICoroutineProvider _coroutineProvider;
        public TileDestroyer(Tile[,] tiles, ICoroutineProvider coroutineProvider)
        {
            _tiles = tiles;
            _coroutineProvider = coroutineProvider;
        }
        public void DestroyHorizontalLine(List<int> matchTiles, int rowIndex)
        {
            foreach (var tileIndex in matchTiles)
            {
                var tile = _tiles[tileIndex, rowIndex];
                DestroyTile(tile);
            }
        }
        public void DestroyVerticalLine(List<int> columnResult, int posX, Action callback)
        {
            foreach (var tileIndex in columnResult)
            {
                var tile = _tiles[posX, tileIndex];
                DestroyTile(tile);
            }
            _coroutineProvider.StartCoroutine(
                CallbackWithDelay(columnResult.Count * AnimationTime, callback));

        }
        IEnumerator CallbackWithDelay(float delayTime, Action callback)
        {
            yield return new WaitForSeconds(delayTime);
            callback?.Invoke();
        }
        void DestroyTile(Tile tile)
        {
            if (tile == null) return;
            _coroutineProvider.StartCoroutine(ZoomIn(tile, () => DestroyOnEndAnimation(tile)));

        }
        IEnumerator ZoomIn(Tile tile, Action action)
        {
            var startTime = Time.time;
            var newScale = tile.transform.localScale;
            while (Time.time - startTime < AnimationTime)
            {
                var delta = 1 - (Time.time - startTime) / AnimationTime;
                tile.transform.localScale = newScale * delta;
                yield return null;
            }
            action?.Invoke();
        }

        void DestroyOnEndAnimation(Tile tile)
        {
            tile.transform.localScale = Vector3.zero;
            var pos = tile.gridPosition;
            Object.Destroy(tile.gameObject);
        }
    }
}