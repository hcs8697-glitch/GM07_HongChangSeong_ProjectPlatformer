using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    void Update()
    {
        if (targetTransform == null) return;
        transform.position = new Vector3(targetTransform.position.x, transform.position.y, -10);
    }
}
