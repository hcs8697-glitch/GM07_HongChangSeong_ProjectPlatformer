using UnityEngine;

//UI상호작용 코드만 모아둘 클래스.
public class PlayerUIAction : MonoBehaviour
{
    [SerializeField] private bool toggleInven;


    // Update is called once per frame
    void Update()
    {
        OnTabPressed();
    }

    //솔직히 죽을 때 빼고 인벤토리 접근은 허용해야 하는데, 죽으면 애초에 Player객체가 사라지잖아?
    //그냥 얘 업데이트에서 돌리는 게?
    //안 눌렀는데도 호출되는 거 생각하면, 그런 거 없애려면 이벤트 버스 같은 거 쓰면 되는 건가?
    public void OnTabPressed()
    {
        if(InputManager.IsTab)
        {
            toggleInven = !toggleInven;

            UIManager.Instance.Inventory.ToggleInventory(toggleInven);
        }


    }

}
