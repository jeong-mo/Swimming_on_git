using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 작성자 : 곽진성
/// 기능 : CMD 창에서 입력을 담당
/// </summary>
public class InputManager : MonoBehaviour
{
    [SerializeField] Text input;            // 입력할 텍스트
    [SerializeField] Text output;           // 출력할 텍스트
    [SerializeField] ScrollRect scroll;     // 출력 스크롤

    private void Start()
    {
        // 처음에 출력 텍스트 초기화
        output.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            ConductInput();
    }

    // 입력 텍스트 처리
    public void ConductInput()
    {
        // 아무것도 없을때 처리
        if (output.text == "")
        {
            output.text = input.text;
            return;
        }

        // 입력 텍스트 출력에 추가
        output.text += '\n';
        output.text += input.text;

        // 입력 텍스트 초기화
        input.text = "";

        // 스크롤 위치 아래로 세팅
        Canvas.ForceUpdateCanvases();
        scroll.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}