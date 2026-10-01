using UnityEngine;


//실제로 무기 역할을 할 게임 오브젝트에 붙여 사용한다.
public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponType weaponType;

    [Header("근접 : 판정 중심점 / 원거리 : 발사 위치")]
    [SerializeField] private Transform attackPoint;

    private Animator weaponAnimator;

    private void Awake()
    {
        weaponAnimator = GetComponent<Animator>();
    }

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PerformAttack()
    {
        if (weaponAnimator = null)
        {
            Debug.Log($"{this.name.ToString()} 무기 프리팹에 애니메이터가 없습니다");
            return;
        }

        //무기 애니메이션 재생
        //TODO : 좀 더 직관적인 방식으로 호출할 수 있도록
        weaponAnimator.SetInteger(1, 1);

        //매개변수로 facingDirection을 건네야 하는데, 대체 어떻게...?
        //weaponType.AttackByType()
    }
}
