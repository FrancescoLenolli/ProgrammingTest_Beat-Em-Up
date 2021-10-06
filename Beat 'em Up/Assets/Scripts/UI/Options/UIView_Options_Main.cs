using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class UIView_Options_Main : UIView
{
    private Action onCloseOptions;

    public Action OnCloseOptions { get => onCloseOptions; set => onCloseOptions = value; }

    public void CloseOptions()
    {
        onCloseOptions?.Invoke();
    }
}
