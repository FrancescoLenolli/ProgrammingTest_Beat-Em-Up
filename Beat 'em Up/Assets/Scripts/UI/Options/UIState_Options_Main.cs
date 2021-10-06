using System.Collections;
using System.Collections.Generic;
using UIFramework;
using UIFramework.StateMachine;
using UnityEngine;

public class UIState_Options_Main : UIState_Options
{
    private UIView_Options_Main view;
    private GameObject rootObject;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = root.ViewMain;
        rootObject = root.gameObject;
        view.OnCloseOptions += CloseOptions;
        Time.timeScale = 0;
    }

    private void CloseOptions()
    {
        Time.timeScale = 1;
        Destroy(rootObject);
    }
}
