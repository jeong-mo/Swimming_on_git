using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 섬 목록 관리
/// (작성자 : 곽진성)
/// </summary>
public class IslandManager : MonoBehaviour
{
    [SerializeField] Island[] islands;  // 화면에 보이는 섬 리스트
    [SerializeField] Transform main;    // 메인 카메라 트랜스폼

    private Vector3 initPosition;   // 카메라 처음 위치
    private Vector3 initRotation;   // 카메라 처음 각도

    private Vector3 targetPosition; // 카메라 이동 위치
    private Vector3 targetRotation; // 카메라 변환 각도

    private void Start()
    {
        // 처음 카메라 각도 및 위치 설정
        initPosition = main.position;
        initRotation = main.rotation.eulerAngles;

        // 목표 각도 및 위치 설정
        targetPosition = initPosition;
        targetRotation = initRotation;

        // 섬 이벤트 추가
        foreach (Island island in islands)
            island.setTarget += SetIsland;
    }

    // 설정 초기화
    public void SetInit()
    {
        targetPosition = initPosition;
        targetRotation = initRotation;
    }

    // 특정 섬 설정
    public void SetIsland(Island selected)
    {
        foreach (Island island in islands)
            island.gameObject.SetActive(island == selected);

        targetPosition = selected.target;
        targetRotation.Set(0, 0, 0);
    }

    private void FixedUpdate()
    {
        // 목표 위치 및 각도로 이동
        main.position = Vector3.Lerp(main.position, targetPosition, Time.deltaTime);
        main.rotation = Quaternion.Euler(Vector3.Lerp(main.rotation.eulerAngles, targetRotation, Time.deltaTime));
    }
}