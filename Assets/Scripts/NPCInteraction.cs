using UnityEngine;

//플레이어와 상호작용할 수 있는 NPC.
//구조 : 플레이어가 이 녀석이랑 상호작용하면 일어날 일
//1) 대화박스 출력
//2) 대화박스에 얘가 들고있는 대화를 하나씩 전달
public class NPCInteraction : MonoBehaviour, IInteractable
{
    [Header("NPC의 이름")]
    [SerializeField] private string NPCname;

    [Header("이 NPC가 들고있는 대화")]
    [SerializeField] private DialogueData data;

    public string Name => NPCname;




    public void Interact()
    {
        if(UIDialoguePanelController.Instance == null)
        {
            Debug.LogWarning("UIDialoguePanelController의 인스턴스를 찾을 수 없음");
            return;
        }




        if(UIDialoguePanelController.Instance != null && data != null)
        {
            UIDialoguePanelController.Instance.StartDialogue(data);
        }
        else
        {
            Debug.Log("상호작용에는 성공했는데 뭔가 없음");
        }
    }
}
