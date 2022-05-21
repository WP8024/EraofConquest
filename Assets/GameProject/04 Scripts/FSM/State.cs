public abstract class State<T> where T : ObjectBody
{
    public abstract void Enter(T entity); //상태 시작시 1회 호출
    public abstract void Execute(T entity);// 상태중 매프레임 호출
    public abstract void Exit(T entity); // 상태 종료시 1회 호출


    public abstract bool OnMessage(T entity, Telegram telegram);
}
