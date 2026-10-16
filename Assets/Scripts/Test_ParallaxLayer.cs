using UnityEngine;

//배경화면 이미지 4개 쓰는 파랄락스.

public class Test_ParallaxLayer : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float parallaxFactor = 0.5f;

    [Header("배경 이미지")]
    [SerializeField] private Transform firstTransform;
    [SerializeField] private Transform secondTransform;
    [SerializeField] private Transform thirdTransform;
    [SerializeField] private Transform fourthTransform;

    [Header("배경 이미지 크기")]
    [SerializeField] private float spriteWidth = 40.0f;
    [SerializeField] private float spriteHeight = 20.0f;

    private Vector3 prevCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
        {
            Camera mainCamera = Camera.main;

            if (mainCamera == null)
            {
                Debug.LogError("Main Camera를 찾을 수 없습니다.", this);
                enabled = false;
                return;
            }

            cameraTransform = mainCamera.transform;
        }

        prevCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        MoveParallax();

        RepeatHorizontal();
        RepeatVertical();

        prevCameraPosition = cameraTransform.position;
    }

    private void MoveParallax()
    {
        Vector3 cameraDelta =
            cameraTransform.position - prevCameraPosition;

        transform.position += new Vector3(
            cameraDelta.x * parallaxFactor,
            cameraDelta.y * parallaxFactor,
            0.0f
        );
    }

    private void RepeatHorizontal()
    {
        float cameraX = cameraTransform.position.x;
        float repeatDistance = spriteWidth * 2f;

        Transform[] tiles =
        {
        firstTransform,
        secondTransform,
        thirdTransform,
        fourthTransform
    };

        foreach (Transform tile in tiles)
        {
            while (cameraX - tile.position.x > spriteWidth)
            {
                tile.position += Vector3.right * repeatDistance;
            }

            while (tile.position.x - cameraX > spriteWidth)
            {
                tile.position += Vector3.left * repeatDistance;
            }
        }
    }

    private void RepeatVertical()
    {
        float cameraY = cameraTransform.position.y;
        float repeatDistance = spriteHeight * 2f;

        Transform[] tiles =
        {
        firstTransform,
        secondTransform,
        thirdTransform,
        fourthTransform
    };

        foreach (Transform tile in tiles)
        {
            while (cameraY - tile.position.y > spriteHeight)
            {
                tile.position += Vector3.up * repeatDistance;
            }

            while (tile.position.y - cameraY > spriteHeight)
            {
                tile.position += Vector3.down * repeatDistance;
            }
        }
    }
}