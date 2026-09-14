//모든 State들이 상속받을 인터페이스.
//메서드 3개를 모두 구현해야 하며, 상태 변화는 IState를 통해 다형성으로 관리한다.

public interface IState 
{
    public void Enter();

    public void Stay();

    public void Exit();
}
