using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update


    IslandEngine tmp;
    void Start()
    {
        tmp = new IslandEngine();

        UnityEngine.Debug.Log("start");
        tmp.startEngine();
        UnityEngine.Debug.Log("startdone");
        tmp.Input("git --help");

        string s = tmp.engineOutput();
        UnityEngine.Debug.Log(s);



        tmp.downEngine();
    }

    // Update is called once per frame
    void Update()
    { 
    }
}