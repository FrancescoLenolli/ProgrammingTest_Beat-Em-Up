using System;
using TMPro;
using UIFramework.StateMachine;
using UnityEngine;
using UnityEngine.UI;

public class UIView_EndGame_Main : UIView
{
    [Tooltip("Message displayed when the Player completes the current level.")]
    [SerializeField]
    private string levelCompletedMessage = "Level Completed!";
    [Tooltip("Message displayed when the Player fails the current level.")]
    [SerializeField]
    private string gameOverMessage = "Game Over!";
    [SerializeField]
    private TextMeshProUGUI endGameMessageLabel = null;
    [Tooltip("Button used to restart level or advance to the next.")]
    [SerializeField]
    private Button restartButton = null;

    // First time using this kind of syntax, bool? accepts null values.
    private bool? isLevelCompleted = null;
    private Action onContinueGame;
    private Action onQuitToMainMenu;
    private Action onQuitGame;

    public Action OnContinueGame { get => onContinueGame; set => onContinueGame = value; }
    public Action OnQuitToMainMenu { get => onQuitToMainMenu; set => onQuitToMainMenu = value; }
    public Action OnQuitGame { get => onQuitGame; set => onQuitGame = value; }

    public override void ShowView()
    {
        if (isLevelCompleted != null)
            base.ShowView();
    }

    public void SetUp(bool levelCompleted)
    {
        isLevelCompleted = levelCompleted;
        endGameMessageLabel.text = (bool)isLevelCompleted ? levelCompletedMessage : gameOverMessage;
        restartButton.GetComponentInChildren<TextMeshProUGUI>().text = (bool)isLevelCompleted ? "CONTINUE" : "RESTART";
    }

    public void ContinueGame()
    {
        onContinueGame?.Invoke();
    }

    public void QuitToMainMenu()
    {
        onQuitToMainMenu?.Invoke();
    }

    public void QuitGame()
    {
        onQuitGame?.Invoke();
    }
}
