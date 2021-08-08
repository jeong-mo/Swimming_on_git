using System.Collections;
using System.Collections.Generic;
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
        git_parser git_p = new git_parser(ttt.ToString());

        git_p.make_branch();
        arr = git_p.sendArrayList();
        git_p.set_begin();

        string[] branch_names = (string[])arr.ToArray(typeof(string));

        for(int i = 0; i < branch_names.Length; i++)
        {
            branch_names[i] = branch_names[i].Trim();        
            temp2 = new IslandEngine();
            temp2.StartEngine();
            temp2.WriteInput("git checkout " + branch_names[i]);
            StringBuilder tmp1 = temp2.ReadOutput();
            while (tmp1 == null)
            {
                tmp1 = temp2.ReadOutput();
            }
            tmp1.Clear();
            temp3 = new IslandEngine();
            temp3.StartEngine();
            temp3.WriteInput("git log --pretty=format:\"%h,%an,%s\"");
            StringBuilder tmp2 = temp3.ReadOutput();
            while (tmp2 == null)
            {
                tmp2 = temp3.ReadOutput();
            }
            UnityEngine.Debug.Log("Done");
            UnityEngine.Debug.Log(tmp2.ToString());
            git_parser git_temp = new git_parser(tmp2.ToString());
            tmp2.Clear();
            git_temp.make_log_json(branch_names[i]);
            git_temp.set_begin();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }
}