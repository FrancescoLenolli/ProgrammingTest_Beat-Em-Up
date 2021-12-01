﻿using UIFramework.StateMachine;

public class UIState_HUD : UIState
{
    protected UIRoot_HUD root;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        if (!root)
            root = (UIRoot_HUD)this.owner.Root;
    }
}
