using UIFramework.StateMachine;
using UnityEngine;

public class UIState_EndGame_Main : UIState_EndGame
{
    UIView_EndGame_Main view;
    LevelManager levelManager;

    public override void PrepareState(UIStateMachine owner)
    {
        base.PrepareState(owner);
        view = root.MainView;
        levelManager = root.LevelManager;
        levelManager.OnLevelCompleted += ShowPanel;
        view.OnContinueGame += Continue;
        view.OnQuitToMainMenu += QuitToMainMenu;
        view.OnQuitGame += QuitGame;
    }

    public override void ShowState()
    {
        view.ShowView();
    }

    public override void HideState()
    {
        view.HideView();
    }

    private void ShowPanel(bool isLevelCompleted)
    {
        view.SetUp(isLevelCompleted);
        ShowState();
    }

    private void Continue()
    {
        HideState();
        levelManager.ContinueGame();
    }

    private void QuitToMainMenu()
    {
        LoadScene(0);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
