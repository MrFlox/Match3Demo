using System.Collections.Generic;
using Core;
using ScriptableObjects;
using SharedComponents;
using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace Match3
{
    [RequireComponent(typeof(ISwiper))]
    public class Tile : MonoBehaviour
    {
        [SerializeField] Image view;
        public Vec2 gridPosition;
        // public readonly static List<Color> Colors = new() { Color.red, Color.green, Color.yellow };
        ISwiper _tileSwiper;
        GameBoard _gameBoard;
        public int CurrentColorIndex;
        GameSettings _settings;
        public Color Color { get; private set; }
        void Awake()
        {
            _settings = Locator.Get<GameSettings>();
            _tileSwiper = GetComponent<ISwiper>();
        }
        void OnEnable() => _tileSwiper.OnSwipe += OnSwipeHandler;
        void OnDisable() => _tileSwiper.OnSwipe -= OnSwipeHandler;
        void OnSwipeHandler(SwipeDirection direction) =>
            _gameBoard.ChangeNeighbours(direction, this);
        public void Init(GameBoard gameBoard) => _gameBoard = gameBoard;
        public void SetColor(int colorIndex)
        {
            CurrentColorIndex = colorIndex;
            Color = _settings.ColorVariants[CurrentColorIndex];
            view.color = Color;
        }
        Color RandomColor()
        {
            CurrentColorIndex = RandomGenerator.Generate();
            return _settings.ColorVariants[CurrentColorIndex];
        }
        public void SwapWithTile(Tile neighbour, Vec2 pos)
        {
            SetTileOnGrid(neighbour, gridPosition);
            SetTileOnGrid(pos);

            var tileTransform = transform;
            var neighbourTransform = neighbour.transform;
            (tileTransform.position, neighbourTransform.position) =
                (neighbourTransform.position, tileTransform.position);
        }
        public void SetTileOnGrid(Tile tile, Vec2 pos)
        {
            _gameBoard.Tiles[pos.X, pos.Y] = tile;
            tile.gridPosition = pos;
            UpdateName();
        }
        public void SetTileOnGrid(Vec2 newPos) => SetTileOnGrid(this, newPos);
        public void UpdateName() => name = $"Tile_{gridPosition.X}_{gridPosition.Y}";
    }
}