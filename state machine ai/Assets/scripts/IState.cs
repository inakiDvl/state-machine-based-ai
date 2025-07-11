public interface IState
{
    public void EnterState();
    public void UpdateState(float deltaTime);
    public void ExitState();
}
