using System.Collections.Generic;
using UIFramework.StateMachine;

public class UIState_HUD_Main : UIState_HUD
{
    private UIView_HUD_Main view;
    private LevelManager levelManager;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);

        view = root.HUDMain;
        levelManager = root.LevelManager;
        view.InitHealthBars(root.Player, levelManager.EnemyCount);
        levelManager = root.LevelManager;
        levelManager.OnStartWave += ShowEnemyBars;
        levelManager.OnEndWave += HideEnemyBars;
    }

    public void ShowEnemyBars(List<EnemyControl> enemies)
    {
        view.ShowEnemyBars(enemies);
    }

    public void HideEnemyBars()
    {
        view.HideEnemyBars();
    }
}
