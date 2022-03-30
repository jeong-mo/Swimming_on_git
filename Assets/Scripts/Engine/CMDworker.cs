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

    /// <summary>
    /// 많은 개선이 필요할 것으로 보임
    /// </summary>
    /// <param name="s"></param>
    public static void input(string s)
    {
        engine.WriteInput(s);
    }

    public static void output()
    {
        StringBuilder builder = engine.ReadOutput();

        while(engine.CheckOutput())
        {
            builder = engine.ReadOutput();
        }

        if(builder != null)
            InputManager.OutputControl(builder.ToString());
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
