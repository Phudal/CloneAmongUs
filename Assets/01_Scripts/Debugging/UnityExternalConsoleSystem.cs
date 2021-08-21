using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Debug = UnityEngine.Debug;

public static class UnityExternalConsoleSystem
{
    private static System.Diagnostics.ProcessStartInfo _proInfo = new System.Diagnostics.ProcessStartInfo();
    private static System.Diagnostics.Process _pro = new System.Diagnostics.Process();

    private static bool _bIsConsoleStarted = false;
    
    public static void ExecuteConsole()
    {
        // 실행할 파일명 입력 -- cmd
        _proInfo.FileName = @"cmd";
        
        // cmd 창 띄우기
        // true : 띄우지 않기 / false : 띄우기
        _proInfo.CreateNoWindow = false;
        _proInfo.UseShellExecute = false;
        
        // cmd 데이터 받기
        _proInfo.RedirectStandardInput = true;
        // cmd 데이터 보내기
        _proInfo.RedirectStandardOutput = true;
        // cmd 오류내용 받기
        _proInfo.RedirectStandardError = true;

        _pro.StartInfo = _proInfo;
        _pro.Start();

        _bIsConsoleStarted = true;
    }

    public static void ConsoleWrite(string msg)
    {
        if (_bIsConsoleStarted)
        {
            _pro.StandardInput.WriteLine(msg);
            Console.WriteLine(msg);
            
        }
    }
    
    public static void KillConsole()
    {
        _pro.StandardInput.Close();
        string resultValue = _pro.StandardOutput.ReadToEnd();
        _pro.WaitForExit();
        _pro.Close();

        _bIsConsoleStarted = false;
    }
}