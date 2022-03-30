using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : 커밋을 표현하는 구름 관리
/// </summary>
public class Cloud : MonoBehaviour
{
    [SerializeField] TextMesh title;    // 커밋 제목
    [SerializeField] TextMesh message;  // 커밋 내용

    // 커밋 제목 적용
    public void SetTitle(string _title)
    {
        title.text = _title;
    }

    // 커밋 메시지 적용
    public void SetMesage(string _message)
    {
        message.text = _message;
    }
}