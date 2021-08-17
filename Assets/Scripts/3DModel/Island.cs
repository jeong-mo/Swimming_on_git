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
    public delegate void SetTarget(Island selected);
    public SetTarget setTarget;

    public TextMesh title;  // 브랜치 이름 표시

    [SerializeField] GameObject group;          // 사람 배치 그룹
    [SerializeField] GameObject contributor;    // 사람 프리팹

    [SerializeField] float distanceY;   // 카메라 Y 거리
    [SerializeField] float distanceZ;   // 카메라 Z 거리
     
    [HideInInspector] public Vector3 initPosition;      // 섬의 현재 위치
    [HideInInspector] public Vector3 targetPosition;    // 섬의 목표 위치
    [HideInInspector] public Vector3 target;            // 섬 전용 카메라 위치

    [SerializeField] Transform top;         // 섬 위에 좌표
    [SerializeField] float distanceCloud;   // 구름 사이의 거리
    [SerializeField] GameObject cloud;      // 커밋 표현 오브젝트
    private List<GameObject> clouds;        // 구름 리스트
    private List<Commit> cloudInformations;  // 커밋 정보 리스트

    private List<Transform> transforms;     // 사람 배치 상태 리스트
    private List<Contributor> contributors; // 브랜치에 있는 사람 리스트
    
    public void Init()
    {
        // 사람 배치 위치 적용
        transforms = new List<Transform>();
        foreach(Transform child in group.GetComponentsInChildren<Transform>())
        {
            if (child.transform == group.transform)
                continue;

            transforms.Add(child);
        }

        contributors = new List<Contributor>();
        clouds = new List<GameObject>();
        cloudInformations = new List<Commit>();
    }

    // 섬 클릭 시 이벤트 실행
    public void OnMouseDown()
    {
        setTarget(this);
    }

    // 처음 및 목표 위치 설정
    public void ApplyTarget()
    {
        target.x = transform.position.x;
        target.y = transform.position.y + distanceY;
        target.z = transform.position.z - distanceZ;

        initPosition = transform.position;
        targetPosition = new Vector3(transform.position.x, transform.position.y - 55, transform.position.z);
    }

    // 섬 이동
    public void MoveIsland(bool fall)
    {
        StopAllCoroutines();
        StartCoroutine(Move(fall));
    }

    // 섬 이동 코루틴
    private IEnumerator Move(bool fall)
    {
        // 섬이 아래로 이동
        if (fall)
        {
            while(transform.position.y > targetPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y - Time.deltaTime * 30f, transform.position.z);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            transform.position = targetPosition;
        }
        // 섬이 위로 이동
        else
        {
            while (transform.position.y < initPosition.y)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y + Time.deltaTime * 30f, transform.position.z);
                yield return new WaitForSeconds(Time.deltaTime);
            }

            transform.position = initPosition;

            // 섬 이름 표시
            yield return new WaitForSeconds(3f);
            title.gameObject.SetActive(true);
        }

        yield return null;
    }

    // 사람 수를 매니저에게 받고 배치
    public void LocateContributor(string[] authors)
    {
        int count = authors.Length;

        // 빈 자리 체크 배열
        bool[] empty = new bool[transforms.Count];
        for (int i = 0; i < empty.Length; i++)
            empty[i] = false;

        // 랜덤 자리 결정
        int people = count > transforms.Count ? transforms.Count : count;
        for(int i = 0; i < people; i++)
        {
            int index;
            do
            {
                index = Random.Range(0, transforms.Count);
            } while (empty[index] == true);
            
            empty[index] = true;
        }

        // 사람 배치
        for (int i = 0; i < people; i++)
        {
            for (int j = 0; j < empty.Length; j++)
            {
                if (!empty[j]) continue;

                // 사람 생성
                GameObject newContributor = Instantiate(contributor);
                newContributor.transform.position = transforms[j].position;
                newContributor.transform.rotation = transforms[j].rotation;
                newContributor.transform.parent = transform;
                newContributor.transform.localScale = contributor.transform.localScale;

                // 사람 정보 적용
                Contributor information = newContributor.GetComponent<Contributor>();
                information.SetName(authors[i]);
                information.ActiveName(false);

                // 컨트리뷰터 목록에 추가
                contributors.Add(information);

                empty[j] = false;
                break;
            }
        }
    }

    // 모든 컨트리뷰터 네임 태그 관리
    public void ActiveAllContributorName(bool active)
    {
        foreach (Contributor contributor in contributors)
            contributor.ActiveName(active);
    }

    // 커밋 정보 적용
    public void ApplyCloudInformation(Commit[] commits)
    {
        for(int i = 0; i < commits.Length; i++)
            cloudInformations.Add(commits[i]);
    }

    // 모든 커밋을 구름으로 생성
    public void MakeCloud()
    {
        // 커밋 인덱스
        int index = 1;

        for(int i = 0; i < cloudInformations.Count; i++)
        {
            // 새로운 구름 생성
            GameObject newCloud = Instantiate(cloud);
            newCloud.transform.position = top.position;
            newCloud.transform.Translate(0, distanceCloud * index, 0);

            // 커밋 정보 적용
            Cloud cloudInformation = newCloud.GetComponent<Cloud>();
            cloudInformation.SetTitle(cloudInformations[i].title);
            cloudInformation.SetMesage(cloudInformations[i].message);

            clouds.Add(newCloud);
            index++;
        }
    }

    // 커밋 구름 모두 삭제
    public void DeleteCloud()
    {
        foreach(GameObject cloud in clouds)
            Destroy(cloud);

        clouds.Clear();
    }
}