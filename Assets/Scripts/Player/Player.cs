using UnityEngine;

//플레이어의 상태를 관장하는 최상위클래스
//상태머신을 만들 때, 이 클래스만 생성자로 전달하고, 나머지는 player.

//그니까 싱글톤은 아마도 아닐 거잖아.
//정적 인스턴스하나 만들면 접근은 편한데 Player.Instance.EWTWETwet
//안 만들면, 필드로 가져야하고.

//player내부 클래스들은 서로 필드를 갖게 해야 할 것인지? 플레이어 외부의 클래스들,
//가령 적이나 UI와 같은 경우에는 이벤트 기반으로 해야  할 것인지
//이벤트 기반으로 한다고 할지라도, 결국에는 정적 인스턴스 안 만들면 필드로 가져야 하거든?



public class Player : MonoBehaviour
{
    //player.Attack 이런 식으로 쓸 수 있도록...?
    private PlayerAnimationController animationController;
    private PlayerAttack attack;
    private PlayerHealth health;
    private PlayerController controller;

    //프로퍼티
    private PlayerAnimationController AnimationController => animationController;
    private PlayerAttack Attack => attack;
    private PlayerHealth Health => health;
    private PlayerController Controller => controller;

    //TODO : 상태머신에서 Player만 딱 건네줄 수 있도록 리팩토링


    private void Awake()
    {
        animationController = GetComponent<PlayerAnimationController>();
        controller = GetComponent<PlayerController>();
        health = GetComponent<PlayerHealth>();
        attack = GetComponent<PlayerAttack>();
    }



}
