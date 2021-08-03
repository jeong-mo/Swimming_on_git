using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update


    IslandEngine tmp;
    void Start()
    {
        tmp = new IslandEngine();

        UnityEngine.Debug.Log("start");

        tmp.StartEngine();


        UnityEngine.Debug.Log("startdone");
        tmp.WriteInput("git --help");

        StringBuilder s = tmp.ReadOutput();


        UnityEngine.Debug.Log(s.Length);
        UnityEngine.Debug.Log(s.ToString());

    }

    // Update is called once per frame
    void Update()
    {
    }
}