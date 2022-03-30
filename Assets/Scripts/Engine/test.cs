using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update


    IslandEngine tmp;
    IslandEngine temp2, temp3;
    ArrayList arr;
    void Start()
    {
        tmp = new IslandEngine();

        tmp.StartEngine();

        tmp.WriteInput("git branch --all");

        StringBuilder ttt = tmp.ReadOutput();

        while (ttt == null)
        {
            ttt = tmp.ReadOutput();
        }
        UnityEngine.Debug.Log("Done");
        UnityEngine.Debug.Log(ttt.ToString());

        git_parser git_p = new git_parser(ttt.ToString());      //처음부터 끝까지 이 git_parser 생성자를 사용할 예정
        git_p.make_branch();
        arr = git_p.sendArrayList();    //이제 arr엔 branch의 명단들이 들어가있음

        string git_repo = tmp.ShowWorkingDirectory();
        string[] git_repo_temp = git_repo.Split(new string[] { "\\" }, StringSplitOptions.None);
        git_p.add_repository_name(git_repo_temp[git_repo_temp.Length - 1].Replace("\r",string.Empty));  //이제 git_p엔 repositoy 이름 저장됨

        string[] branch_names = (string[])arr.ToArray(typeof(string));

        for(int i = 0; i < branch_names.Length; i++)
        {
            branch_names[i] = branch_names[i].Trim();
            tmp.WriteInput("git checkout " + branch_names[i]);    //git log찍을 branch 변경
            StringBuilder tmp1 = tmp.ReadOutput();
            while (tmp1 == null)
            {
                tmp1 = tmp.ReadOutput();
            }
            tmp1.Clear();
            tmp.WriteInput("git log --pretty=format:%an");         //author명단 출력
            StringBuilder tmp2 = tmp.ReadOutput();
            while (tmp2 == null)
            {
                tmp2 = tmp.ReadOutput();
            }
            UnityEngine.Debug.Log("Done");
            UnityEngine.Debug.Log(tmp2.ToString());
            git_p.set_parse_original(tmp2.ToString());
            git_p.add_branch_log(branch_names[i]);
            tmp2.Clear();
        }
        git_p.make_json();
    }

    // Update is called once per frame
    void Update()
    {
    }
}