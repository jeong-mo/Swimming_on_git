using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 브랜치 병합 전에 감지
/// </summary>
public class Detectable : MonoBehaviour
{
    [SerializeField] Material normal;
    [SerializeField] Material highlight;

    private MeshRenderer current;

    // "임시" 텍스트
    [SerializeField] GameObject text;

    private void Start()
    {
        current = GetComponent<MeshRenderer>();
        current.material = normal;
    }

    private void OnTriggerEnter(Collider other)
    {
        current.material = highlight;
    }

    private void OnTriggerExit(Collider other)
    {
        current.material = normal;

        // "임시" 로 병합 글씨만 표시
        text.SetActive(true);
    }
}