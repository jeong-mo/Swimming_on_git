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
    private string repository_name;
    ArrayList arrayList = new ArrayList();          //branch 명단 저장됨
    ArrayList arrayList2 = new ArrayList();         //author 명단 저장됨
    JObject final_jObject = new JObject();          //최종적으로 json파일의 모든것이 저장되는 JObject
    JArray branches = new JArray();
    public git_parser(string parse_original)        //초기 생성자
    {
        this.parse_original = parse_original;
    }

    public void set_parse_original(string parse_original)   //각 branch별로 얻어온 Log로 parse_original을 수정후 작업
    {
        this.parse_original = parse_original;
    }

    public void make_branch()       //브랜치 명단을 생성해서 arrayList에 저장
    {
        string[] temp = parse_original.Split('\n');         //이안에 브랜치이름 입력됨
        for(int i = 0; i < temp.Length - 1; i++)
        {
            if (temp[i].Contains("->") == true)
            {

            }
            else if (temp[i].Contains("remotes/origin/") == true)
            {
                arrayList.Add(temp[i].Substring(17).Replace("\r", string.Empty));
            }
            else if (temp[i].Contains("remotes/origin/") == false && temp[i].Contains(" -> ") == false)
            {
                arrayList.Add(temp[i].Substring(2).Replace("\r", string.Empty));
            }
        }
    }

    public void add_repository_name(string repo_name)
    {
        this.repository_name = repo_name;
        JObject tmp = new JObject();
        final_jObject.Add("title", repo_name);
    }

    public void add_branch_log(string branch_name)        //특정 브랜치에 있는 author의 이름 및 브랜치명을 json파일에 저장
    {
        ArrayList arrayList3 = new ArrayList();
        JObject jObject_temp = new JObject();
        JArray jArray = new JArray();
        HashSet<string> hash = new HashSet<string>();
        string[] temp = parse_original.Split('\n');
        for (int i = 0; i < temp.Length - 1; i++)
        {
            UnityEngine.Debug.Log(temp[i]);
            hash.Add(temp[i].Replace("\r",string.Empty));
        }

        foreach(object name in hash)
        {
            arrayList3.Add(name.ToString());
        }

        string[] author_names = (string[])arrayList3.ToArray(typeof(string));

        for (int i = 0; i < author_names.Length; i++)
        {
            JObject j_temp = new JObject();
            j_temp.Add("name", author_names[i]);
            jArray.Add(j_temp);
        }

        jObject_temp.Add("title", branch_name);
        jObject_temp.Add("author", jArray);
        branches.Add(jObject_temp);
    }



    public void make_json()         //저장한 json 구조체를 토대로 json파일을 실제로 생성. 이때 파일 이름을 해당 저장소 이름으로 지정
    {
        final_jObject.Add("branch", branches);
        string[] location = { "D:\\testing\\", repository_name, ".json" };  //앞부분만 원하는 곳으로 json파일 생성경로 지정

        string location_real = string.Concat(location);

        if (!File.Exists(location_real))
        {
            File.WriteAllText(location_real, final_jObject.ToString());
        }
        else
        {
            File.Delete(location_real);
            File.WriteAllText(location_real, final_jObject.ToString());
        }
    }

    public void set_begin()         //세팅한 author의 이름들을 초기화
    {
        arrayList2 = new ArrayList();
    }

    public ArrayList sendArrayList()//만들어둔 branch 명단을 우선적으로 test.cs(미래의 메인 프로세스 진행 부분)에 전달
    {
        return arrayList;
    }

}
