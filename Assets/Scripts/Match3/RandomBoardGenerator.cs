using System.Collections.Generic;
using UnityEngine;

namespace Match3
{
    public class RandomBoardGenerator
    {
        readonly int _boardSize;
        readonly int _randomColorsCount;
        public int[,] result;
        public RandomBoardGenerator(int boardSize, int randomColorsCount)
        {
            _boardSize = boardSize;
            _randomColorsCount = randomColorsCount;
            result = new int[boardSize, boardSize];
            Generate();
        }

        public void Generate()
        {
            var lastValue = -1;
            for (var y = 0; y < _boardSize; y++)
            {
                for (var x = 0; x < _boardSize; x++)
                {
                    int newValue;
                    if (x == 0)
                        newValue = GetRandomValue(lastValue);
                    else
                        newValue = y > 0
                            ? GetRandomValue(lastValue, result[x, y - 1])
                            : GetRandomValue(lastValue);
                    result[x, y] = newValue;
                    lastValue = newValue;
                }
            }
        }
        int GetRandomValue(int lastValue, int lastRowValue)
        {
            List<int> tmpRange = new List<int>();
            for (int i = 0; i < _randomColorsCount; i++)
            {
                if (i != lastValue && i != lastRowValue)
                    tmpRange.Add(i);
            }
            return tmpRange[Random.Range(0, tmpRange.Count)];
        }
        int GetRandomValue(int lastValue) => GetRandomValue(lastValue, -1);
    }
}