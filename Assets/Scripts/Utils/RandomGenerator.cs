namespace Utils
{
    public static class RandomGenerator
    {
        static int _lastValue = -1;
        public static int Generate()
        {
            var value = GenRandomValue();
            if (_lastValue != -1)
            {
                while (value == _lastValue)
                    value = GenRandomValue();
            }
            _lastValue = value;
            return value;
        }
        static int GenRandomValue() => UnityEngine.Random.Range(0, 3);
    }
}