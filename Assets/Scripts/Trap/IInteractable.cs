//상호작용할 수 있는 모든 클래스에 붙일 인터페이스
//스위치나 NPC, 떨어진 아이템 등에 넣게 될 것.


interface IInteractable
{
    //상호작용할 대상의 이름.
    //DroppedItem클래스에서는 SO의 이름을 갖고오게 하면 될 것이다.
    string Name { get; }

    
    void Interact();
}
