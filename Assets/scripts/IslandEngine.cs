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
    public static StringBuilder outputString = null;

    private Process bash = new Process();
    private ProcessStartInfo bashInfo = new ProcessStartInfo();

    private StreamWriter writer;


    /// <summary>
    /// 
    /// </summary>
    /// <param name="s"></param>
    public void SetFileName(string s)
    {
        bashInfo.FileName = s;
        bash.StartInfo = bashInfo;
    }
    /// <summary>
    /// 
    /// </summary>
    private void SetInfo()
    {
        bashInfo.FileName = "C:\\Program Files (x86)\\Git\\bin\\bash.exe";
        bashInfo.UseShellExecute = false;

        bashInfo.RedirectStandardOutput = true;
        outputString = new StringBuilder();


        bashInfo.RedirectStandardInput = true;

        bash.StartInfo = bashInfo;
        bash.OutputDataReceived += OutputHandler;

    }

    /// <summary>
    /// 
    /// </summary>
    public void StartEngine()
    {
        SetInfo();
        bash.Start();
        bash.BeginOutputReadLine();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="input"></param>
    public void WriteInput(string input)
    {
        writer = bash.StandardInput;
        writer.WriteLine(input);
        writer.Close();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public StringBuilder ReadOutput()
    {
        // 이거 호출이 너무 빨라서 아무것도 없는게 감
        return outputString;
    }

    /// <summary>
    /// 
    /// </summary>
    public void StopEngine()
    {
        bash.Close();
    }


    /// <summary>
    /// 
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
            outputString.Append(outLine.Data);

            // test
            UnityEngine.Debug.Log(outLine.Data);
        }
    }

}