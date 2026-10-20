using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;


//DialoguePanel에 직접 붙여서 사용할 스크립트.
//대화문 데이터를 무언가를 통해 전달받으면, 그것들을 맞는 필드에 띄운다.
//또한, 해당 패널을 클릭했을 시 대화문을 넘기게 한다.
public class UIDialoguePanelController : MonoBehaviour, IPointerDownHandler
{
    public static UIDialoguePanelController Instance { get; private set; }

    //어쨌든 씬마다 일일이 설정해줘야 하는 수고가 있는데, 그거는 사실 프리팹화하면 될 일 아닌가?


    [Header("이름 출력부")]
    [SerializeField] private TMP_Text speakerNameText;
    [Header("대사 출력부")]
    [SerializeField] private TMP_Text dialogueLineText;
    [Header("현재 진행할 대사")]
    [SerializeField] private DialogueData dialogueData;
    [SerializeField] private int dialogueCount;


    //이전 인스턴스가 남지 않도록 OnDestroy에서 Instance를 null로 바꾸는 것도 고려한다.

    private void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            Debug.Log($"[UIDialoguePanelController] : 인스턴스 설정 완료. {Instance.gameObject.name}");
        }
        //싱글톤은 적용하지 않고, 전역 호출만 갱신한다.


        CheckComponent();
        gameObject.SetActive(false);
    }

    //이것도 솔직히 필요없음. 프리팹화할거면 이미 내부에서 다 등록되어 있음.
    private bool CheckComponent()
    {
        if (speakerNameText == null)
        {
            Debug.LogWarning("[UIDialoguePanelController] : 이름 출력 텍스트가 등록되지 않았습니다");
            return false;

            
        }

        if (dialogueLineText == null)
        {
            Debug.LogWarning("[UIDialoguePanelController] : 이름 출력 텍스트가 등록되지 않았습니다");
            return false;
        }

        return true;
    }


    //매개변수로 전달받은 데이터를 셋팅하고, 1회 띄운다.
    public void StartDialogue(DialogueData dialogueData)
    {
        if(dialogueData == null || dialogueData.Dialogues.Count <= 0)
        {
            Debug.LogWarning($"[UIDialoguePanelController] : 전달받은 dialogueData {dialogueData.name}이 올바르게 설정되어 있지 않습니다");
            return;
        }


        this.dialogueData = dialogueData;
        dialogueCount = 0;
        gameObject.SetActive(true);


        RefreshDialoguePanel();

    }


    public void OnPointerDown(PointerEventData eventData)
    {
        dialogueCount++;

        if (dialogueData.Dialogues.Count > dialogueCount)
        {
            RefreshDialoguePanel();
        }
        else
        {
            ResetDialoguePanel();
        }
    }

    //대화 패널의 내용을 갱신하는 메서드
    //뭔가 특정 처리, 가령 대화패널이 흔들린다거나, 사운드가 다르게 출력되어야 한다면
    //데이터의 DialogueLine 클래스에 필드를 추가하고, 여기서 검사하는 방식이어야 함.
    //근데, 뭐 이모티콘 띄우는 거야 할 수 있겠는데, 실제 게임 오브젝트가 뭔가 흔들린다거나 이런 처리는 어떻게 할까?
    //말하는 대상도 Transform매개변수를 받게 하고, this 키워드로 조작한다? 이렇게 하면 될 것 같은데?
    //아니 근데, 애매하네 이거. DialogueLine에서 그런 거 다 관리해야 할 것 같은데, "몇 번 째 대사에서만 특정 처리를 해야 할 것"인데?
    public void RefreshDialoguePanel()
    {
        speakerNameText.text = dialogueData.Dialogues[dialogueCount].speaker.ToString();

        dialogueLineText.text = dialogueData.Dialogues[dialogueCount].line;
    }


    //대화를 종료하게 될 때 내용들을 싹 다 날릴 메서드
    public void ResetDialoguePanel()
    {
        dialogueData = null;
        speakerNameText.text = "";
        dialogueLineText.text = "";
        dialogueCount = 0;
        gameObject.SetActive(false);
    }
}
