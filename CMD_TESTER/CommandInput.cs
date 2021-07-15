using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using System.ComponentModel;
using static System.Console;

namespace UnityNOOBTrying
{
    class CommandInput : Process
    {
        public CommandInput()
        {
            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = "CMD.exe";
            startInfo.UseShellExecute = false;
            startInfo.RedirectStandardError = true;
            startInfo.RedirectStandardInput = true;
            startInfo.RedirectStandardOutput = true;
            base.EnableRaisingEvents = false;
            base.StartInfo = startInfo;
            Executor();
        }

        //cmd 콘솔 진행. 어지간한 명령어는 다 듣는듯.
        //base.StartInfo.WorkingDirectory에 최초 cmd시작지점 지정해둘 수 있음
        public void Executor()
        {
            base.StartInfo.WorkingDirectory = @"C:\";
            Write(base.StartInfo.WorkingDirectory + ">");
            while (true)
            {
                string cmd = Console.ReadLine();
                if("exit".Equals(cmd.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    break;
                }
                try
                {
                    base.Start();
                    using (base.StandardInput)
                    {
                        base.StandardInput.Write(cmd + Environment.NewLine);
                    }
                    using (base.StandardError)
                    {
                        var tmp = base.StandardError.ReadToEnd();
                        if (!String.IsNullOrWhiteSpace(tmp))
                        {
                            Console.WriteLine(tmp);
                        }
                    }
                    using (base.StandardOutput)
                    {
                        var tmp = base.StandardOutput.ReadToEnd();
                        cmd += "\r\n";
                        Console.Write(tmp.Substring(tmp.IndexOf(cmd) + cmd.Length));
                        String buffer = tmp.Substring(tmp.LastIndexOf("\r\n\r\n") + 4);
                        base.StartInfo.WorkingDirectory = buffer.Substring(0, buffer.Length - 1);
                    }
                }
                catch(Exception e)
                {
                    base.StartInfo.WorkingDirectory = @"C:\";
                    Console.WriteLine(e);
                }
                finally
                {
                    base.Close();
                }
            }
        }
        static void Main(String[] args)
        {
            new CommandInput();
            //근데 cmd를 종료할 때 exit 명령어 먹인 다음 엔터를 2번 눌러야한다는 문제점이 아닌 문제점 존재
            Console.WriteLine("Press Any Key...!");
            Console.ReadKey();
        }
    }
}
