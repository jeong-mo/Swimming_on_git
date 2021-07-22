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

    [HideInInspector] public Vector3 initPosition;      // 섬의 현재 위치
    [HideInInspector] public Vector3 targetPosition;    // 섬의 목표 위치
    [HideInInspector] public Vector3 target;            // 섬 전용 카메라 위치
    
    private void Start()
    {
        target.x = transform.position.x;
        target.y = transform.position.y;
        target.z = transform.position.z - 300;

        // 현재 및 목표 위치 설정
        initPosition = transform.position;
        targetPosition = new Vector3(transform.position.x, transform.position.y - 55, transform.position.z);
    }

    // 섬 클릭 시 이벤트 실행
    public void OnMouseDown()
    {
        setTarget(this);
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
}