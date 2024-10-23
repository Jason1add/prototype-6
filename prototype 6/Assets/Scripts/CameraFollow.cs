using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 要跟随的目标
    public float smoothSpeed = 0.125f; // 跟随的平滑速度
    public Vector3 offset; // 相机与目标之间的偏移量

    void LateUpdate()
    {
        if (target != null)
        {
            // 目标位置加上偏移量
            Vector3 desiredPosition = target.position + offset;
            // 平滑地插值到目标位置
            Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            transform.position = smoothedPosition;
        }
    }
}

