using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class environmentTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string a = Environment.GetEnvironmentVariable("path");
        IDictionary b = Environment.GetEnvironmentVariables();

        UnityEngine.Debug.Log(a);
        //UnityEngine.Debug.Log(a.Split(';'));
        foreach (string e in a.Split(';'))
        {
            //UnityEngine.Debug.Log(e);
            if (e.Contains("Git") || e.Contains("git"))
            {
                UnityEngine.Debug.Log(e);

                string [] ab = e.Split('\\');
                foreach(string k in ab)
                {
                    UnityEngine.Debug.Log(k);
                    // 재조합해서 경로 지정하면 된다.
                    // 다른 컴퓨터에서도 되는지 확인해야된다.
                }
            }
        }
        //foreach (DictionaryEntry de in b)
        //{
        //    UnityEngine.Debug.Log(de.Key);
        //    UnityEngine.Debug.Log(de.Value);
        //}
    }

// Update is called once per frame
void Update()

    {

    }
}
