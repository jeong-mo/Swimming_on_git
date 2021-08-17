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

    private static Text cmdText;   // 출력 텍스트
    private static List<GameObject> outputList; // 출력 문장 리스트

    public static GameObject outputContent; // 출력 문장을 붙일 콘텐츠
    public static bool isUpdateScroll = false;

    private void Start()
    {
        // 처음에 출력 텍스트 초기화
        // output.text = "";
        // output_text = GameObject.Find("Log").GetComponent<Text>();

        cmdText = Resources.Load<GameObject>("CMD/Log").GetComponent<Text>();
        outputContent = GameObject.Find("Content");

        // 출력 문장 모두 초기화
        for(int i = 0; i < outputContent.transform.childCount; i++)
            Destroy(outputContent.transform.GetChild(i));

        outputList = new List<GameObject>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
            ConductInput();

        CMDworker.output();

        if (isUpdateScroll)
        {
            UpdateScroll();
            isUpdateScroll = false;
        }
    }

    public static void OutputControl(string s)
    {
        // 입력받은 텍스트 추가
        GameObject newText = Instantiate(cmdText.gameObject);
        newText.GetComponent<Text>().text = s;
        newText.transform.parent = outputContent.transform;

        // 문장 리스트에 추가
        outputList.Add(newText);

        //output_text.text += s;
        isUpdateScroll = true;
    }

    // 입력 텍스트 처리
    public void ConductInput()
    {
        //// 아무것도 없을때 처리
        //if (output.text == "")
        //{
        //    output.text = input.text;
        //    return;
        //}

        CMDworker.input(input.text);
        UnityEngine.Debug.Log(input.text);

        // 입력 텍스트 출력에 추가
        //output.text += '\n';
        //output.text += input.text;
        //output.text += '\n';
        // 입력 텍스트 초기화
        input.text = "";

        // 스크롤 위치 아래로 세팅
        UpdateScroll();
    }

    public void UpdateScroll()
    {
        Canvas.ForceUpdateCanvases();
        scroll.verticalNormalizedPosition = 0f;
        Canvas.ForceUpdateCanvases();
    }
}