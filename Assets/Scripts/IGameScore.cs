using Core;

public interface IGameScore : IService
{
    public ReactiveProperty<int> Score { get; }
    void AddScore(int value);
}