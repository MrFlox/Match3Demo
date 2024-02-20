using System.Collections.Generic;
using Core;
using Match3;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Match3/GameSettings", order = 0)]
    public class GameSettings : ScriptableObject, IService
    {
        public float MatchRoundTime = 60.0f;
        public int BoardSize;
        public List<Color> ColorVariants;
        public Tile tilePrefab;
        public float tileSize;
    }
}