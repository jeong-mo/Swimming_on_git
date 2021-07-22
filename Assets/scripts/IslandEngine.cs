using System.Collections;
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

    private Process process = new Process();
    private ProcessStartInfo processStartInfo = new ProcessStartInfo();
    StreamWriter writer;
    StreamReader reader;


    // 비동기 아웃풋 받아오기
    private static StringBuilder output;

    private void startInfo()
    {
        // 프로세스 시작시 사용할 창 상태 설정 hidden
        processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;

        // git 설치 시에 같이 설치되는 bash 위치
        processStartInfo.FileName = @"C:\Program Files (x86)\Git\bin\bash.exe";


        // 프로세스 시작할 때 운영체제 셸을 사용할지 여부
        // 해당 옵션이 false여야지 리다이렉트 가능
        // false로 설정되어있지않으면 working directory도 exe파일 위치로 고정됨
        processStartInfo.UseShellExecute = false;

        // 에러 인풋 아웃풋 리다이렉트
        processStartInfo.RedirectStandardError = true;
        processStartInfo.RedirectStandardInput = true;
        processStartInfo.RedirectStandardOutput = true;

    

        // TODO :: error 처리
    }



    // https://docs.microsoft.com/ko-kr/dotnet/api/system.diagnostics.process.beginoutputreadline?view=net-5.0#System_Diagnostics_Process_BeginOutputReadLine
    // 여기서 긁어온거라 잘 모름
    private static void OutputHandler(object sendingProcess,
            DataReceivedEventArgs outLine)
    {
        // Collect the sort command output.
        if (!String.IsNullOrEmpty(outLine.Data))
        {
            // Add the text to the collected output.
            output.Append(Environment.NewLine + outLine.Data);
        }
    }


    public void Input(string s)
    {

        UnityEngine.Debug.Log(s);
        writer.WriteLine(s);

        // 비동기 아웃풋 읽기 시작
        process.BeginErrorReadLine();
    }

    public string engineOutput()
    {
        string s;
        s = reader.ReadToEnd();
        return s;

    }

    // working directory 설정
    public void setting()
    {
        processStartInfo.WorkingDirectory = @"C:/";
        process.StartInfo = processStartInfo;

        // 비동기 아웃풋 세팅
        output = new StringBuilder();
        process.OutputDataReceived += OutputHandler;
    }


    public void startEngine()
    {
        // 기본 startInfo
        startInfo();
        UnityEngine.Debug.Log("startinfo");
        // working directory 설정
        setting();
        UnityEngine.Debug.Log("setting");
        process.Start();
        UnityEngine.Debug.Log("Start");

        // input output 처리
        writer = process.StandardInput;
        reader = process.StandardOutput;
    }

    public void downEngine()
    {
        // 종료 시퀸스
        writer.Close();
        writer.WriteLine("exit");
        process.WaitForExit();
    }


    //public void test()
    //{
    //    Process process = new Process();

    //    ProcessStartInfo processStartInfo = new ProcessStartInfo();
    //    processStartInfo.WindowStyle = ProcessWindowStyle.Hidden;
    //    processStartInfo.FileName = @"C:\Program Files (x86)\Git\bin\bash.exe";
    //    processStartInfo.WorkingDirectory = @"C:/";
    //    //processStartInfo.Arguments = "dir";
    //    processStartInfo.RedirectStandardInput = true;
    //    processStartInfo.RedirectStandardOutput = true;
    //    processStartInfo.RedirectStandardError = true;    
    //    processStartInfo.UseShellExecute = false;

    //    process.StartInfo = processStartInfo;
    //    process.Start();

    //    StreamWriter mySW = process.StandardInput;
    //    StreamReader mySR = process.StandardOutput;

    //    mySW.WriteLine("dir");
    //    mySW.WriteLine("exit");
    //    string output = mySR.ReadToEnd();
    //    string error = process.StandardError.ReadToEnd();

    //    UnityEngine.Debug.Log("output" + output.ToString());

    //    UnityEngine.Debug.Log(error);
    //    //ViewBag.Error = error;
    //    //ViewBag.Ouput = output;
    //}
}