using UIFramework.StateMachine;
using UnityEngine;

public class UIRoot_HUD : UIRoot
{
    [SerializeField]
    private UIView_HUD_Main hudMain = null;
    [SerializeField]
    private LevelManager levelManager = null;
    [SerializeField]
    private PlayerControl player = null;

    public UIView_HUD_Main ViewMain { get => hudMain; }
    public LevelManager LevelManager { get => levelManager; }
    public PlayerControl Player { get => player; }
}
