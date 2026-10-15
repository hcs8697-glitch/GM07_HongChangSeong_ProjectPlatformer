using UnityEditor.Build;
using UnityEngine;

//플레이어와 상호작용할 수 있는 NPC.
//구조 : 플레이어가 이 녀석이랑 상호작용하면 일어날 일
//1) 대화박스 출력
//2) 대화박스에 얘가 들고있는 대화를 하나씩 전달
public class NPCInteraction : MonoBehaviour, IInteractable
{
    [Header("NPC의 이름")]
    [SerializeField] private string NPCname;

    public string Name => NPCname;

    public string[] dialogues;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        //
    }
}
