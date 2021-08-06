using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 브랜치의 컨트리뷰터의 정보 관리
/// </summary>
public class Contributor : MonoBehaviour
{
    [SerializeField] TextMesh author;   // 컨트리뷰터 이름

    // 컨트리뷰터 이름 설정
    public void SetName(string name)
    {
        author.text = name;
    }

    // 컨트리뷰터 이름 정보 표시 조정
    public void ActiveName(bool active)
    {
        author.gameObject.SetActive(active);
    }
}