using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Debug = UnityEngine.Debug;

public static class UN_LOG
{
    // __LINE__
    private static System.Diagnostics.StackFrame _stackFrameForLine = new System.Diagnostics.StackFrame(true);

    // __FILE__
    private static System.Diagnostics.StackFrame _stackFrameForFile = new System.Diagnostics.StackFrame(true);

    // 로그 파일 생성 경로
    static public string LogPath = "";
    
    
    public static string __LINE__([CallerLineNumber] int line = 0)
    {
        return string.Intern($"{line}");
        // return stackFrameForLine.GetFileLineNumber().ToString();
    }

    public static string __FILE__([CallerFilePath] string file = "")
    {
        return string.Intern($"{Path.GetFileName(file)}");
        // return stackFrameForFile.GetFileName();
    }

    // 로그 경로 설정
    private static void SetLogPath()
    {
        // 폴더 이름
        string directoryName = "/Log";
        
        // 프로그램을 실행할 때 마다 별도의 로그 폴더 및 파일 생성 
        directoryName = directoryName + "/" +
                        DateTime.Now.Month + "M " +
                        DateTime.Now.Day + "D " +
                        DateTime.Now.Hour + "H " +
                        DateTime.Now.Minute + "M " +
                        DateTime.Now.Second + "S executed";
        
        
        
        // 로그 파일 이름
        string fileName = "Log.txt";
        
        // 폴더 경로를 저장하기 위한 변수
        string path = "";

        path = (Application.dataPath + directoryName);
        LogPath = (Application.dataPath + directoryName + "/" + fileName);

        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
    }

    public static void Log(string msg)
    {
        // 시스템 로그 기록
        Debug.Log(msg);
        
        // 파일 경로가 만들어지지 않은 상태라면 파일경로 생성 함수로 생성
        if (LogPath == "")
        {
            SetLogPath();
        }

        FileStream tFile = null;
        
        // 파일 확인 후 없다면 생성
        if (!File.Exists(LogPath))
        {
            tFile = new FileStream(LogPath, FileMode.Create, FileAccess.Write);
        }
        
        // 파일이 있으면 내용 추가
        else
        {
            tFile = new FileStream(LogPath, FileMode.Append);
        }
        
        // 열린 파일이 크기가 크디면 닫고 새 파일 스트림으로 생성
        if (tFile.Length > 1048000)
        {
            tFile.Close();
            tFile = new FileStream(LogPath, FileMode.Create, FileAccess.Write);
        }

        StreamWriter tSW = new StreamWriter(tFile);
        
        // 로그 내용 앞에 시간 추가
        string tLogfrm = DateTime.Now.ToString("MM-dd HH::mm::ss") + " -- " + msg;
        
        // 로그 기록
        tSW.WriteLine(tLogfrm);
        
        // 사용했던 스트림들 닫기
        tSW.Close();
        tFile.Close();
    }

    public static void LogWithFileName(string msg, [CallerFilePath] string file = "")
    {
        // Log("File: " + __FILE__() + " | " + _msg);
        Log("File: " + string.Intern($"{Path.GetFileName(file)}") + " | " + msg);
    }

    public static void LogWithLineNumber(string msg, [CallerLineNumber] int line = 0)
    {
        Log("Line: " + string.Intern($"{line}") + " | " + msg);
    }

    public static void LogWithFileAndLine(string msg, 
        [CallerFilePath] string file = "",
        [CallerLineNumber] int line = 0)
    {
        // Log("File: " + __FILE__() + " | " + "Line: " + __LINE__() + " | " + _msg);
        Log("File: " + string.Intern($"{Path.GetFileName(file)}") + " | " +
            "Line: " + string.Intern($"{line}") + " | " + msg);
    }
    
    
}


