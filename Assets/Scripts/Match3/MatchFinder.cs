using System.Collections.Generic;

namespace Match3
{
    public static class MatchFinder
    {
        public static List<int> FindConsecutiveMatches(int[] nums)
        {
            var result = new List<int>();

            int lastValue = -1;
            for (int i = 0; i < nums.Length; i++)
            {
                int curValue = nums[i];
                if (i > 0)
                {
                    if (lastValue == curValue)
                    {
                        if (result.Count == 0) result.Add(i - 1);
                        result.Add(i);
                    }
                    else
                    {
                        if (result.Count < 3) result.Clear();
                    }
                }
                lastValue = curValue;
            }
            if (result.Count < 3) result.Clear();
            return result;
        }
    }
}