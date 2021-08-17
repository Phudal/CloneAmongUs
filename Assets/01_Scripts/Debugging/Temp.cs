using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Temp : MonoBehaviour
{
    private void Start()
    {
        UN_LOG.LogWithFileName("this is start - file");
        UN_LOG.LogWithLineNumber("this is start - line");
        UN_LOG.LogWithFileAndLine("this is start - file and line");
    }

    // this is temp func
    /*
    private void Update()
    {
        Debug.Log(UN_LOG.__FILE__());

        Debug.Log(UN_LOG.__LINE__());
    } */ 
}
