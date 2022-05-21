public abstract class State<T> where T : ObjectBody
{
    public abstract void Enter(T entity); //���� ���۽� 1ȸ ȣ��
    public abstract void Execute(T entity);// ������ �������� ȣ��
    public abstract void Exit(T entity); // ���� ����� 1ȸ ȣ��


    public abstract bool OnMessage(T entity, Telegram telegram);
}
