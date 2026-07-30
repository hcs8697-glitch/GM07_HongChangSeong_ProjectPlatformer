using UnityEngine;
using UnityEngine.InputSystem;


//입력을 처리할 스크립트.
//아마도 나중에  gameManager 구현 후 싱글톤으로 적용할 것.

public class InputManager : MonoBehaviour
{
    //입력 액션 필드들.
    private InputAction moveAction;
    private InputAction jumpAction;

    //외부에서 사용할 정적 프로퍼티
    public static Vector2 Movement { get; private set; } = Vector2.zero;
    public static bool IsJump { get; private set; } = false;

    public static bool IsLeftClicked { get; private set; } = false;

    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        jumpAction = InputSystem.actions.FindAction("Jump");
    }

    void Update()
    {
        Movement = moveAction.ReadValue<Vector2>();
        IsJump = jumpAction.WasPressedThisFrame();
        IsLeftClicked = Mouse.current.leftButton.wasPressedThisFrame;
    }
}
