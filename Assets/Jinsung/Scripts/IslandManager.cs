using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Information
{
    public string title;
    public int people;
}

/// <summary>
/// 섬 목록 관리
/// (작성자 : 곽진성)
/// </summary>
public class IslandManager : MonoBehaviour
{
    [SerializeField] Transform main;    // 메인 카메라 트랜스폼
    [SerializeField] Camera mainCamera; // 메인 카메라
    [SerializeField] GameObject back;   // 뒤로 가기 버튼

    private Vector3 initPosition;   // 카메라 처음 위치
    private Vector3 initRotation;   // 카메라 처음 각도
    
    private Vector3 targetPosition; // 카메라 이동 위치
    private Vector3 targetRotation; // 카메라 변환 각도

    [SerializeField] float initSize;    // 카메라 처음 크기
    [SerializeField] float finalSize;   // 카메라 최종 크기
    private float targetSize;           // 카메라 목표 크기
    
    [SerializeField] Information[] informations;    // 섬 정보 리스트
    [SerializeField] GameObject islandPrefab;       // 섬 오브젝트 프리팹
    [SerializeField] float distance;                // 섬 사이의 거리
    [SerializeField] Transform start;               // 섬 시작 위치

    private List<Island> islands;   // 화면에 보이는 섬 리스트

    private void Start()
    {
        // 처음 카메라 각도 및 위치 설정
        initPosition = main.position;
        initRotation = main.rotation.eulerAngles;

        // 목표 각도 및 위치 설정
        targetPosition = initPosition;
        targetRotation = initRotation;

        targetSize = initSize;
        
        islands = new List<Island>();

        // 섬 스크립트 불러옴
        for(int i = 0; i < informations.Length; i++)
        {
            GameObject newIsland = Instantiate(islandPrefab);

            // 섬 정보 적용
            Island newInformation = newIsland.GetComponent<Island>();
            newInformation.Init();
            newInformation.title.text = informations[i].title;
            newInformation.LocateContributor(informations[i].people);
            islands.Add(newInformation);
        }

        // 섬 이벤트 추가
        foreach (Island island in islands)
            island.setTarget += SetIsland;

        // 첫 섬은 중앙에 위치
        islands[0].transform.position = start.transform.position;
        islands[0].transform.localScale.Set(1, 1, 1);
        islands[0].Init();
        islands[0].LocateContributor(informations[0].people);

        // 섬 위치 조정
        float degree = 360f / (islands.Count - 1);
        for(int i = 0; i < islands.Count - 1; i++)
        {
            // Y는 임시로 설정
            islands[i + 1].transform.position = new Vector3(start.transform.position.x + (Mathf.Cos(Mathf.Deg2Rad * (degree * i)) * distance), -13, start.transform.position.z + (Mathf.Sin(Mathf.Deg2Rad * (degree * i)) * distance));
            islands[i + 1].transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            islands[i + 1].Init();
            islands[i + 1].LocateContributor(informations[i + 1].people);
        }

        // 뒤로 가기 버튼 숨기기
        back.SetActive(false);
    }

    // 설정 초기화
    public void SetInit()
    {
        targetPosition = initPosition;
        targetRotation = initRotation;

        targetSize = initSize;

        // 섬의 위치 조정
        foreach (Island island in islands)
            island.MoveIsland(false);
    
        back.SetActive(false);
    }

    // 특정 섬 설정
    public void SetIsland(Island selected)
    {
        // 해당 섬이 아니면 섬 가라 앉음
        foreach (Island island in islands)
        {
            // 섬 이름 숨김
            island.title.gameObject.SetActive(false);

            if (island != selected)
                island.MoveIsland(true);
        }
        
        targetPosition = selected.target;
        targetRotation.Set(0, 0, 0);

        targetSize = finalSize;

        back.SetActive(true);
    }

    private void FixedUpdate()
    {
        // 목표 위치 및 각도로 이동
        main.position = Vector3.Lerp(main.position, targetPosition, Time.deltaTime);
        main.rotation = Quaternion.Euler(Vector3.Lerp(main.rotation.eulerAngles, targetRotation, Time.deltaTime));
        mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, targetSize, Time.deltaTime);
    }
}