using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;
using System.IO;
using System;
using System.Text;
using System.Threading;
using System.ComponentModel;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

public class git_parser
{
    private string parse_original;
    ArrayList arrayList = new ArrayList();
    ArrayList arrayList2 = new ArrayList();
    public git_parser(string parse_original)
    {
        this.parse_original = parse_original;
    }

    public void make_branch()
    {
        string[] temp = parse_original.Split('\n');         //이안에 브랜치이름 입력됨
        for(int i = 0; i < temp.Length - 1; i++)
        {
            if (temp[i].Contains("->") == true)
            {

            }
            else if (temp[i].Contains("remotes/origin/") == true)
            {
                arrayList.Add(temp[i].Substring(17));
            }
            else if(temp[i].Contains("remotes/origin/") == false && temp[i].Contains(" -> ") == false)
            {
                arrayList.Add(temp[i].Substring(2));
            }
        }
    }

    public void make_log_json(string b)
    {
        JArray jArray = new JArray();
        string[] temp = parse_original.Split('\n');
        for (int i = 0; i < temp.Length; i++)
        {
            arrayList2.Add(temp[i]);
        }

        int cnt = arrayList2.Count;
        if (cnt > 1)
        {
            for (int i = 0; i < cnt - 1; i++)
            {
                string[] temp2 = arrayList2[i].ToString().Split(',');    //temp의 0, 1, 2번째 인덱스에 정보 저장됨
                JObject j_temp = new JObject(
                    new JProperty("hash", temp2[0]),
                    new JProperty("committer", temp2[1]),
                    new JProperty("message", temp2[2]));
                jArray.Add(j_temp);
            }
        }


        string branch_name = b;
        string[] location = { "D:\\testing\\", branch_name, ".json" };

        string location_real = string.Concat(location);
        UnityEngine.Debug.Log(location_real);

        if (!File.Exists(location_real))
        {
            File.WriteAllText(location_real, jArray.ToString());
        }
        else
        {
            File.Delete(location_real);
            File.WriteAllText(location_real, jArray.ToString());
        }
    }

    public void set_begin()
    {
        arrayList = new ArrayList();
    }

    public ArrayList sendArrayList()
    {
        return arrayList;
    }

}
