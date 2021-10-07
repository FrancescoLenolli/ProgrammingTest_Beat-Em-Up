using System.Collections.Generic;
using UIFramework.StateMachine;

public class UIState_HUD_Main : UIState_HUD
{
    private UIView_HUD_Main view;
    private LevelManager levelManager;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);

        view = root.ViewMain;
        levelManager = root.LevelManager;
        view.InitHealthBars(root.Player, levelManager.EnemyCount);
        levelManager = root.LevelManager;
        levelManager.OnStartWave += ShowEnemyBars;
        levelManager.OnEndWave += HideEnemyBars;
        levelManager.OnLevelReset += ResetHUD;
    }

    public void ShowEnemyBars(List<EnemyControl> enemies)
    {
        view.ShowEnemyBars(enemies);
    }

    public void HideEnemyBars()
    {
        view.HideEnemyBars();
    }

    private void ResetHUD()
    {
        view.ResetHUD();
    }
}
