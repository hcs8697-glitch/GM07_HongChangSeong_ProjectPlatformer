using UnityEngine;

//감지 영역 내에서 플레이어가 상호작용 키를 누르면, 특정 행동을 실행할 클래스

public class Switch : MonoBehaviour
{
    [Header("스위치로 조작할 대상")]
    [SerializeField] private Switchable[] switchables;



    //OnTriggerStay2D를 쓰지 않고, 업데이트에서 플레이어를 감지할 수 있도록 바꿔야 IInteractable 인터페이스를 상속받을 수 있을 것
    private void OnTriggerStay2D(Collider2D collision)
    {
        //임시 : 감지범위 내부에 플레이어가 들어온 상태에서 좌클릭이 눌렸을 경우
        if(collision.CompareTag("Player") && InputManager.IsLeftClicked)
        {
            for (int i = 0; i < switchables.Length; i++)
            {
                if (switchables[i] == null)
                {
                    Debug.LogWarning($"[Switch] : 조작할 배열의 {i}번째 요소가 null입니다.");
                    continue;
                }
                switchables[i].OnSwitchPressed();
            }
        }
    }
}
