using Core;

namespace Infrastructure
{
    public interface IStateManager: IService
    {
        void SetState(StateMachine.States state);
    }
}