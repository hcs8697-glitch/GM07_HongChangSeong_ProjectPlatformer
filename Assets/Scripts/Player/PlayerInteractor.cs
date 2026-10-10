using UnityEngine;

//플레이어 주변에서, IInteractable 컴포넌트를 가진 개체를 지속적으로 탐색하고, 들어왔을 시에 상호작용 키를 눌러 실행하게 할 스크립트
public class PlayerInteractor : MonoBehaviour
{
    [Header("감지범위")]
    [SerializeField] private float checkRadius = 1f;
    [Header("감지할 레이어")]
    [SerializeField] private LayerMask interactionLayer;

    //현재 할당된 상호작용할 객체
    private IInteractable curInteractable;
   

    
    void Update()
    {
        CheckInteratable();
    }

    //조건에 따라서 감지한 것들 중 가장 플레이어에게서 가까운 것을 위의 curInteratable에 전달
    private void CheckInteratable()
    {
        //레이어마스크를 써야 할 것인지?
        //아무튼 무슨 방식으로 감지할 것임

        //2D에서는 NonAlloc 사용 불가
        Collider2D hit = Physics2D.OverlapCircle(transform.position, checkRadius, interactionLayer);

        /*
       
        [가장 가까운 대상 탐색하게 하는 방법]
        //영역 내부안에 들어온 것들을 전부 배열화
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, checkRadius, interactionLayer);

        //빈 변수를 선언하고, 최댓값을 가진 float 변수를 선언한다
        IInteractable closestInteractable = null;
        float closestDistance = float.MaxValue;

        //foreach문을 통해서 가장 가까운 거리에 있는 대상을 탐색한다
        foreach(Collider2D h in hits)
        {
            //InteractionLayer지만, IInteractable이 없는 애는 뺀다... 인데, 그럴 경우가... 있을 수도 있긴 하지만 있나?
            if(!h.TryGetComponent<IInteractable>(out var candidate))
            {
                continue;
            }

            //ClosestPoint : (특정위치)와 해당 콜라이더의 표면에서 가장 가까운 점의 좌표를 계산해서 반환해주는 빌트인 메서드
            //그 점과 플레이어의 위치를 비교한다
            float distance = Vector2.Distance(transform.position, h.ClosestPoint(transform.position));

            //만약 그 거리가 적으면
            if(distance < closestDistance)
            {
                //거리는 그걸로 하고 우선 후보를 가장 가까운대상으로 선정한다.
                closestDistance = distance;
                closestInteractable = candidate;
            }
        }

        //foreach문을 나서면 가장 가까운 후보가 정해졌으므로 그것을 상호작용할 것으로 정한다
        curInteractable = closestInteractable;
        */
        

        //뭔가를 감지했고 + 거기에 컴포넌트가 있으면 할당하고
         if (hit != null && hit.TryGetComponent<IInteractable>(out curInteractable))
         {
             Debug.Log($"현재 상호작용할 대상 : {curInteractable.Name}");
             //여기에서 상호작용할 대상을 테두리처리 하는 게 맞을지?
         }
         else //감지하지 못했거나 없으면은 null로
         {
             //Debug.Log("상호작용할 대상 없음");
             curInteractable = null;
         }
    }

    //상호작용 키를 입력하는 부분은 퍼사드에서 조립하여 상태머신의 각 상태에서 구현한다.
    public void OnPressedInteractionKey()
    {
        if (curInteractable == null) return;

        curInteractable.Interact();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;

        Gizmos.DrawWireSphere(transform.position, checkRadius);
       
    }
}
