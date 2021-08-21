using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private void Start()
    {
        /*
        UN_LOG.LogWithFileName("this is start - file");
        UN_LOG.LogWithLineNumber("this is start - line");
        UN_LOG.LogWithFileAndLine("this is start - file and line");
        
        Debug.Log("msg");
        
        Debug.Log(Application.dataPath + "../");
        Debug.Log(Application.consoleLogPath); 
        */
        
        /*
        UnityExternalConsoleSystem.ExecuteConsole();

        UnityExternalConsoleSystem.ConsoleWrite("msg1");
        UnityExternalConsoleSystem.ConsoleWrite("msg2");
        */
    }

    private void OnDestroy()
    {
        // UnityExternalConsoleSystem.KillConsole();
    }
}
