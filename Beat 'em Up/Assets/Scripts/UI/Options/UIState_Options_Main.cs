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
    }

    public override void ShowState()
    {
        view.ShowView();
    }

    public override void HideState()
    {
        view.HideView();
    }

    private void CloseOptions()
    {
        Time.timeScale = 1;
        HideState();
        UIManager.Instance.OptionsOpen = false;
    }
}
