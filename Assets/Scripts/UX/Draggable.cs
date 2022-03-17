using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 브랜치 드래그
/// </summary>
public class Draggable : MonoBehaviour
{
    [SerializeField] Material normal;
    [SerializeField] Material stealth;

    private MeshRenderer current;

    private Vector3 start;
    private Vector3 point;

    private void Start()
    {
        start = transform.position;
        current = GetComponent<MeshRenderer>();
    }

    // 브랜치와 같은 지평선의 좌표
    private Vector3 RatioApplied(Vector3 point)
    {
        float ratio = (Camera.main.transform.position.y - start.y) / (Camera.main.transform.position.y - point.y);

        float resultX = (point.x - Camera.main.transform.position.x) * ratio + Camera.main.transform.position.x;
        float resultY = start.y;
        float resultZ = (point.z - Camera.main.transform.position.z) * ratio + Camera.main.transform.position.z;

        return new Vector3(resultX, resultY, resultZ);
    }

    private void OnMouseDown()
    {
        // 보이게 설정
        current.material = normal;
    }

    private void OnMouseDrag()
    {
        // 마우스 위치 계산
        point = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.transform.position.z));
        transform.position = RatioApplied(point);
    }

    private void OnMouseUp()
    {
        // 다시 숨기기
        current.material = stealth;
        transform.position = start;
    }
}