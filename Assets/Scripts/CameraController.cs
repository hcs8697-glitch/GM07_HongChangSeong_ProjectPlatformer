using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    void LateUpdate()
    {
        if (targetTransform == null) return;
        transform.position = new Vector3(targetTransform.position.x, targetTransform.position.y+3, -10);
    }
}
