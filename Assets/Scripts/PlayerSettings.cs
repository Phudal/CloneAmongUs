using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 플레이어 설정에 관한 스크립트
public class PlayerSettings
{
    private static EControlType controlType;

    public static EControlType GetControlMode()
    {
        return controlType;
    }
    
    // Set ControlMode overload
    public static void SetControlMode(int _controlType)
    {
        controlType = (EControlType) _controlType;
    }

    public static void SetControlMode(EControlType _controlType)
    {
        controlType = _controlType;
    }
}
