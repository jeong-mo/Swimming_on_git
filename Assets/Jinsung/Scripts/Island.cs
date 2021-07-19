using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 섬 객체 관련
/// (작성자 : 곽진성)
/// </summary>
public class Island : MonoBehaviour
{
    // 섬 목표 설정
    public delegate void SetTarget(Vector3 position);
    public SetTarget setTarget;

    // 섬 전용 카메라 위치
    private Vector3 target;

    private void Start()
    {
        target.x = transform.position.x;
        target.y = transform.position.y;
        target.z = transform.position.z - 10;
    }

    // 섬 클릭 시 이벤트 실행
    public void OnMouseDown()
    {
        setTarget(target);
    }
}