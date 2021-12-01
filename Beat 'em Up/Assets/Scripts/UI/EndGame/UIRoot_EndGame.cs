using UIFramework.StateMachine;
using UnityEngine;

public class UIRoot_EndGame : UIRoot
{
    [SerializeField]
    private UIView_EndGame_Main optionsMain = null;
    [SerializeField]
    private LevelManager levelManager = null;

    public UIView_EndGame_Main MainView { get => optionsMain; }
    public LevelManager LevelManager { get => levelManager; }
}
