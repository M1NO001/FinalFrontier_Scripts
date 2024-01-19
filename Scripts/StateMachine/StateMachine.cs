public abstract class StateMachine
{

    public IState previousState;
    protected IState currentState;

    public void ChangeState(IState newState)
    {
        currentState?.Exit();
        
        previousState = currentState;

        currentState = newState;

        currentState?.Enter();
    }

    public void HandleInput()
    {
        currentState?.HandleInput();
    }

    public void Update()
    {
        currentState?.Update();
    }
    
    public void LateUpdate()
    {
        currentState?.LateUpdate();
    }

    public void PhysicsUpdate()
    {
        currentState?.PhysicsUpdate();
    }
}