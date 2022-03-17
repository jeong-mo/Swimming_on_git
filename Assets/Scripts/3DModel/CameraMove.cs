using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 작성자 : 곽진성
/// 기능 : 카메라 움직임을 담당
/// </summary>
public class CameraMove : MonoBehaviour
{
    [SerializeField] Transform main;

    [SerializeField] Transform start;
    [SerializeField] Transform end;

    // 임시 flag
    bool flag = false;

    private void Start()
    {
        main.transform.position = start.position;
        main.transform.rotation = start.rotation;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            flag = true;
        
        if (!flag) return;

        main.transform.position = Vector3.Lerp(main.transform.position, end.position, Time.deltaTime);
        main.transform.rotation = Quaternion.Lerp(main.transform.rotation, end.rotation, Time.deltaTime);
    }
}
