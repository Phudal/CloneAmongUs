using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using System.Runtime.CompilerServices;

public static class DebugClass
{
    // __LINE__
    private static System.Diagnostics.StackFrame stackFrameForLine = new System.Diagnostics.StackFrame(true);

    // __FILE__
    private static System.Diagnostics.StackFrame stackFrameForFile = new System.Diagnostics.StackFrame(true);

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
}
