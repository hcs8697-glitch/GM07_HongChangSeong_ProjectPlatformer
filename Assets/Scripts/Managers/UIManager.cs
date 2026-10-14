using UnityEngine;

//모든 UI 관련 매니저들에게 접근할 수 있게하는 싱글톤 클래스
//그런데, UI가 없는 타이틀 화면으로 가버리면 어떡함?
//싱글톤으로 하지 말고 그냥 Instance = this만 하는 게?
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [SerializeField] private UIInventoryManager inventory;

    public UIInventoryManager Inventory => inventory;




    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        EnsureComponent();
    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //시작 시점에선 모든 UI를 꺼두기
    void Start()
    {
        inventory.ToggleInventory(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void EnsureComponent()
    {
        if (inventory == null)
        {
            inventory = FindFirstObjectByType<UIInventoryManager>();
        }


    }

    



}
