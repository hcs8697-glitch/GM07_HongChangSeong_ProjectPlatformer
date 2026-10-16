using UnityEngine;

public class ParallaxLayer : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float parallaxFactor = 0.5f; //원하는 배경 이미지마다 속도 다르게 하면 원근감 조성 가능
    [SerializeField] private Transform firstTransform;
    [SerializeField] private Transform secondTransform;

    [SerializeField] private float spriteWidth = 20.0f;// 미리 스프라이트의 너비를 구해둘 것.

    private Vector3 prevCameraPosition;


    void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        prevCameraPosition = cameraTransform.position;
    }

    // Update is called once per frame
    private void LateUpdate()
    {
        MoveParallax();

        Repeat();

        prevCameraPosition = cameraTransform.position;
    }

    private void MoveParallax()
    {
        Vector3 cameraDelta = cameraTransform.position - prevCameraPosition;

        //transform.position += new Vector3(cameraDelta.x * parallaxFactor, 0.0f, 0.0f);

        //y축 이동도 반영하는 것 테스트
        transform.position += new Vector3(
    cameraDelta.x * parallaxFactor,
    cameraDelta.y * parallaxFactor,
    0.0f
);
    }

    private void Repeat()
    {
        float cameraX = cameraTransform.position.x;

        if (cameraX - firstTransform.position.x > spriteWidth)
        {
            firstTransform.position = new Vector3(secondTransform.position.x + spriteWidth,
                firstTransform.position.y,
                firstTransform.position.z
                );
        }
        if (cameraX - secondTransform.position.x > spriteWidth)
        {
            secondTransform.position = new Vector3(firstTransform.position.x + spriteWidth,
                secondTransform.position.y,
                secondTransform.position.z
                );
        }

        if (firstTransform.position.x - cameraX > spriteWidth)
        {
            firstTransform.position = new Vector3(secondTransform.position.x - spriteWidth,
                firstTransform.position.y,
                firstTransform.position.z
                );
        }
        if (secondTransform.position.x - cameraX > spriteWidth)
        {
            secondTransform.position = new Vector3(firstTransform.position.x - spriteWidth,
                secondTransform.position.y,
                secondTransform.position.z
                );
        }
    }
}
