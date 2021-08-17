﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;
using System.Text;
using System.Threading;
using System.ComponentModel;


public class IslandEngine
{
    public static StringBuilder outputString = null;

    private Process bash = new Process();
    private ProcessStartInfo bashInfo = new ProcessStartInfo();

    private StreamWriter writer;

    private Boolean isOutput = false;


    /// <summary>
    /// 배쉬 위치가 예상한 곳이 아니라면 실행할 깃 배쉬의 위치를 입력
    /// </summary>
    /// <param name="s"></param>
    public void SetFileName(string s)
    {
        bashInfo.FileName = s;
        bash.StartInfo = bashInfo;
    }

    /// <summary>
    /// git bash를 실행시켜서 조작할 directory 경로를 설정
    /// </summary>
    /// <param name="s">경로값</param>
    public void SetWorkingDirectory(string s)
    {
        bashInfo.WorkingDirectory = s;
        bash.StartInfo = bashInfo;
    }

    /// <summary>
    /// 기본적인 시작 설정값
    /// </summary>
    private void SetInfo()
    {
        //bashInfo.FileName = "C:\\Program Files (x86)\\Git\\bin\\bash.exe";
        bashInfo.FileName = "C:\\Program Files\\Git\\bin\\bash.exe";
        bashInfo.UseShellExecute = false;
        bashInfo.CreateNoWindow = true;

        // 테스트용 경로임 삭제할 것
        //bashInfo.WorkingDirectory = "C:\\Users\\jay09\\Desktop\\Swimming_on_git";
        bashInfo.WorkingDirectory = "D:\\UnityProject\\Capstone\\Swimming_on_git";

        bashInfo.RedirectStandardOutput = true;
        outputString = new StringBuilder();


        bashInfo.RedirectStandardInput = true;

        bash.StartInfo = bashInfo;
        bash.OutputDataReceived += OutputHandler;

    }

    /// <summary>
    /// 깃 배쉬를 실행시키고, output을 읽기 시작
    /// </summary>
    public void StartEngine()
    {
        SetInfo();
        bash.Start();
        bash.BeginOutputReadLine();
    }

    /// <summary>
    /// 깃 배쉬에서 실행시킬 명령어를 넣고 실행한다
    /// </summary>
    /// <param name="input"> 깃 배쉬에 입력할 명령어를 그대로 넣을 것</param>
    public void WriteInput(string input)
    {
        outputString.Clear();
        writer = bash.StandardInput;
        writer.WriteLine(input);
        writer.Flush();
    }

    /// <summary>
    /// 현재 쓸 수 없음
    /// output이 string으로 저장되는 속도보다 이걸 호출하는게 빠르면 아무것도 없는게 전달됨
    /// 
    /// TODO :: OutputHandler에서 outputString에 원하는 정보가 들어갔는지 확인하고 호출해야함
    /// </summary>
    /// <returns></returns>
    public StringBuilder ReadOutput()
    {
        // 이거 호출이 너무 빨라서 아무것도 없는게 감
        if (CheckOutput())
        {
            isOutput = false;
            return outputString;
        }
        else
            return null;
    }

    /// <summary>
    /// 깃 배쉬 종료한다
    /// </summary>
    public void StopEngine()
    {
        writer.Close();
        bash.Close();
    }

    /// <summary>
    /// output에 값이 들어있는지 확인한다.
    /// </summary>
    /// <returns>output에 값이 있으면 true, 없다면 false</returns>
    public Boolean CheckOutput()
    {
        return isOutput;
    }


    /// <summary>
    /// 깃 배쉬의 output을 가져와서 무엇을 할 것인가~
    /// </summary>
    /// <param name="sendingProcess"></param>
    /// <param name="outLine"></param>
    private void OutputHandler(object sendingProcess,
            DataReceivedEventArgs outLine)
    {

        // Collect the sort command output.
        if (!String.IsNullOrEmpty(outLine.Data))
        {
            // Add the text to the collected output.
            //UnityEngine.Debug.Log(outLine.Data);
            outputString.AppendLine(outLine.Data);    
            isOutput = true;
        }
    }

}