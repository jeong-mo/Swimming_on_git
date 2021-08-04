using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class CMDworker : MonoBehaviour
{

    public static IslandEngine engine;

    // Start is called before the first frame update
    void Start()
    {
        engine = new IslandEngine();

        engine.StartEngine();

        engine.ReadOutput();
    }

    public static void input(string s)
    {
        engine.WriteInput(s);
    }

    public static string output()
    {
        StringBuilder builder = engine.ReadOutput();

        while(builder == null)
        {
            builder = engine.ReadOutput();
        }

        return engine.ReadOutput().ToString();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
