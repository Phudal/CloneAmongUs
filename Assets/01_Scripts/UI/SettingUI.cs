using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 설정 UI 에 관한 스크립트
public class SettingUI : MonoBehaviour
{
    [SerializeField] private Button MouseControllButton;

    [SerializeField] private Button KeyboardMouseControlButton;

    private Animator _animator;
    private static readonly int _Close = Animator.StringToHash("Close");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        UpdatingButtonImage();
    }

    private void UpdatingButtonImage()
    {
        switch (PlayerSettings.GetControlMode())
        {
            case EControlType.Mouse:
                MouseControllButton.image.color = Color.green;
                KeyboardMouseControlButton.image.color = Color.white;
                break;
            
            case EControlType.KeyboardMouse:
                MouseControllButton.image.color = Color.white;
                KeyboardMouseControlButton.image.color = Color.green;
                break;
        }
    }

    public virtual void Close()
    {
        StartCoroutine(CloseAfterDelay());
    }

    public IEnumerator CloseAfterDelay()
    {
        _animator.SetTrigger(_Close);
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
        _animator.ResetTrigger(_Close);
    }
    
    // 버튼 등록을 위해서 재구현
    public void SetControlMode(int _controlType)
    {
        PlayerSettings.SetControlMode((EControlType) _controlType);
        
        UpdatingButtonImage();
    }
}
