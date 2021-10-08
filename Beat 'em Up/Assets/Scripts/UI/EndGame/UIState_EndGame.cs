using UIFramework.StateMachine;

public class UIState_EndGame : UIState
{
    protected UIRoot_EndGame root;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        if (!root)
            root = (UIRoot_EndGame)owner.Root;
    }
}
