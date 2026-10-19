using System.Collections.Generic;
using UnityEngine;

#region C#에서 했던 방식(보류)
/*
public class DialogueLine
{
    public Speaker speaker;
    public string line;

    public DialogueLine(Speaker speaker, string line)
    {
        this.speaker = speaker;
        this.line = line;
    }

}

public class Dialogues : MonoBehaviour
{



    public List<DialogueLine> lines;



    public virtual void Talk()
    {
    }
}

public class Dialgoue1 : Dialogues
{
    public override void Talk()
    {
        DialogueLine line = new DialogueLine(Speaker.Player, "안녕하세요.");
        lines.Add(line);
        DialogueLine line2 = new DialogueLine(Speaker.NPC1, "누구세요? 처음 보는 분이시네요");
        lines.Add(line2);
    }
}
*/
#endregion

//대사 한 줄, 해당 대사를 말하는 화자, 해당 화자의 스프라이트를 저장하는 클래스
//화자의 이미지가 여러 개 떠야 할 텐데 그건 또 어떻게 할까요? 그건 나중에 해 제발

[System.Serializable]
public class DialogueLine
{
    public Speaker speaker;
    public Sprite speakerIcon;
    [TextArea(3, 8)]public string line;
    

}




//1회의 대화문을 저장해둘 클래스
//특정 플래그에 따라 편집할 수 있으려면 public이어야 할 것인데 그건 나중에 고려할 것.
[CreateAssetMenu(fileName = "NewDialogue", menuName = "Game Data / Dialogue")]
public class DialogueData : ScriptableObject
{
    [Header("대화문")]
    [SerializeField]private List<DialogueLine> dialogues;

    public List<DialogueLine> Dialogues => dialogues;

    //여기에 "대화문을 모두 읽었을 때 실행할 수 있는 메서드를 구현하는 건 어때?"
    //이벤트를 활용하는 방법은 맞을 것 같은데, 어떻게 할 지는 고민해봐야 할 듯.
}

