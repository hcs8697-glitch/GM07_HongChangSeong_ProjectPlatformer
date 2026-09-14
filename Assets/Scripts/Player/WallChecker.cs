using UnityEngine;

//플레이어가 공중에 떠 있는 상태에서 벽에 붙을 수 있는지 아닌지를 판별하게 할 스크립트
//기본 기조는 GroundChecker와 비슷하지만, GrounChecker와는 다르게 이 녀석은 실제로 플레이어가 바라보는 방향으로 체크를 변경해야한다.
//이거... 어떻게 하냐 근데.

//두 가지 방법이 있었을 것 같음
//1) 어차피 position을 조정하니까, 반대방향 시 포지션을 바꾸는 것
//2) 그릴 영역을 바꾸는 것
//생각해보니 그냥 PlayerAttack이랑 동일한 기조로 작성하면 되기는 될 걸?

//얘가 그라운드체커를 갖게 하고, 그라운드체커에서 IsGrounded = false일 때까지 고려해서 벽 붙는 판정을 활성화해야 할 듯?

public class WallChecker : MonoBehaviour
{
    [Header("벽 체크 영역 조절")]
    [SerializeField]private Vector3 checkerSize;
    [Header("탐색할 레이어")]
    [SerializeField] private LayerMask groundLayers; //wall 레이어를 따로 만들 이유가 있을까 싶은데?

    private PlayerController playerController; //이거를 그라운드체커를 그냥 체커로 바꾸고 이걸 추가하는게? 그래야 GetComponent하기 편한데

    //아니면 최대한 인스펙터에서 넣는 방식으로 하고, GetComponent는 어디까지나 안전장치로서 기능하도록?

    private void Awake()
    {      
        playerController = GetComponentInParent<PlayerController>();

        if(playerController == null)
        {
            Debug.LogError("[WallChecker] : 부모 오브젝트에게서 컨트롤러를 찾아오지 못했습니다");
            enabled = false;
            return;
        }
    }


    void FixedUpdate()
    {
        
    }

    //생각해보면, 이 클래스 내부의 필드를 바꾸는 게 아니라, 아예 반환값이 bool이어서 다른 클래스에서 호출하게 하는 게 맞지 않았을까
    //일단은... 이게 어떻게 기능해야 하지?
    //벽에 붙을 수 있는 상태면 초록색, 벽에 붙을 수 없는 상태면 빨간색으로 일단은 보여야 할 것 같은데.
    private void CheckWall()
    {

    }
}
