using UnityEngine;


//스위치를 통해 조작 가능한 클래스들에게 상속할 추상 클래스.
//인터페이스는 인스펙터에서 직렬화되지 않으므로 추상 클래스를 활용한다.
public abstract class Switchable : MonoBehaviour
{
    public abstract void OnSwitchPressed();
}