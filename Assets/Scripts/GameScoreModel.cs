public class GameScoreModel : IGameScore
{
    public ReactiveProperty<int> Score => score;
    ReactiveProperty<int> score = new(0);
    public void AddScore(int resultCount) => score.Value += resultCount;
}