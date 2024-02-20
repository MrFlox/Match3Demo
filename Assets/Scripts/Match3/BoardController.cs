using Core;
using ScriptableObjects;
using UI;
using UnityEngine;

namespace Match3
{
    public class BoardController : MonoBehaviour, ICoroutineProvider, IBoardController
    {
        int _rows;
        int _cols;
        Tile _tilePrefab;
        GameBoard _gameBoard;
        float _startX;
        float _startY;
        float _gridWidth;
        float _gridHeight;
        RandomBoardGenerator _randomBoardGenerator;
        GameSettings _settings;

        void Awake() => Init();

        void Init()
        {
            _settings = Locator.Get<GameSettings>();
            _gameBoard = new GameBoard(this);
            _cols = _settings.BoardSize;
            _rows = _settings.BoardSize;
            _gridWidth = _cols * _settings.tileSize + (_cols - 1);
            _gridHeight = _rows * _settings.tileSize + (_rows - 1);

            _startX = -_gridWidth / 2f;
            _startY = _gridHeight / 2f;

            _randomBoardGenerator = new RandomBoardGenerator(_cols, _settings.ColorVariants.Count);
            GenerateTiles();
        }
        public void GenerateTiles()
        {
            for (var y = 0; y < _rows; y++)
            for (var x = 0; x < _cols; x++)
                AddTile(x, y);
        }
        public void AddTile(int x, int y)
        {
            var tile = Instantiate(_settings.tilePrefab, transform, false);
            tile.GetComponent<RectTransform>().anchoredPosition =
                new Vector2(_startX + x * _settings.tileSize, _startY - y * _settings.tileSize);
            tile.gridPosition = new Vec2(x, y);
            _gameBoard.AddTile(tile);
            tile.Init(_gameBoard);
            tile.SetColor(_randomBoardGenerator.result[x, y]);
        }
        public ICoroutineProvider GetCoroutineProvider() => this;
    }
}