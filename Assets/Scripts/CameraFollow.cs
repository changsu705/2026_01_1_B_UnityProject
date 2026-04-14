using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // 카메라가 따라갈 대상 
    public Vector3 offset = new Vector3(0, 5,-10); // 대상과 카메라 사이의 오프셋
    public float smoothSpeed = 0.125f; // 카메라 이동의 부드러움 정도

    private void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset; // 대상 위치에 오프셋을 더하여 원하는 카메라 위치 계산
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed); // 현재 카메라 위치에서 원하는 위치로 부드럽게 이동
        transform.position = smoothedPosition; // 카메라 위치 업데이트
        transform.LookAt(target); // 카메라가 대상을 바라보도록 설정
    }
}
    