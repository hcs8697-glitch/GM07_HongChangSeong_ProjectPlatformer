using TMPro;
using UnityEngine;


//전달받은 대화문의 정보를 읽어, 대사와 화자 이름 등을 출력하고, 클릭을 통해 입력을 받게 할 스크립트
public class UIDialogueManager : MonoBehaviour
{
    [Header("대화 패널")]
    [SerializeField] private RectTransform dialoguePanel;
    [Header("이름 출력부")]
    [SerializeField] private TMP_Text speakerNameText;
    [Header("대사 출력부")]
    [SerializeField] private TMP_Text dialogueLineText;
    [Header("현재 진행할 대사")]
    [SerializeField] private DialogueData dialogueData;


    private void Awake()
    {
        CloseDialogue();
    }


    public void CloseDialogue()
    {
        speakerNameText.text = "";
        dialogueLineText.text = "";
        dialogueData = null;
        dialoguePanel.gameObject.SetActive(false);
    }

    public void StartDialogue(DialogueData dialogueData)
    {
        this.dialogueData = dialogueData;

        dialoguePanel.gameObject.SetActive(true);

        for (int i = 0; i < dialogueData.Dialogues.Count; i++)
        {
            speakerNameText.text = dialogueData.Dialogues[i].speaker.ToString();

            dialogueLineText.text = dialogueData.Dialogues[i].line;

            /*
            if (dialogueData.Dialogues[i].speakerIcon != null)
            {

            }
            */



        }


    }

}
