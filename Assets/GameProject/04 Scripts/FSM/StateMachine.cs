
public class StateMachine<T> where T : ObjectBody
{
    //stateMachine 소유주
    private T ownerObject;
    //현재,이전,전역 상태 저장
    private State<T> currentState;
    private State<T> previousState;
    private State<T> globalState;


    public void SetUp(T owner,State<T> entryState)
    {
        ownerObject  = owner;
        currentState = null;
        previousState = null;
        globalState = null;

        ChangeState(entryState);
    }

    public void Execute()
    {
        if (globalState != null)
        {
            globalState.Execute(ownerObject);
        }
        if (currentState != null)
        {
            currentState.Execute(ownerObject);
        }
    }
    public void ChangeState(State<T> newState)
    {
        if (newState == null) return;

        if (currentState != null)
        {
            previousState = currentState;
            currentState.Exit(ownerObject);
        }

        currentState = newState;

    }
    public void SetGlobalState(State<T> newState)
    {
        globalState = newState;
    }

    public void RevertToPreviousState()
    {
        ChangeState(previousState);
    }

    public bool HandleMessage(Telegram telegram)
    {
        if (globalState != null && globalState.OnMessage(ownerObject, telegram))
        {
            return true;
        }

        if (currentState != null && currentState.OnMessage(ownerObject, telegram))
        {
            return true;
        }

        return false;
    }
}