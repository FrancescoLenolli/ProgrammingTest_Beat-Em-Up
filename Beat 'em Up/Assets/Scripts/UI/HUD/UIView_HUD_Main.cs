using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class UIView_HUD_Main : UIView
{
    [SerializeField]
    private StatusBar statusBarPrefab = null;
    [SerializeField]
    private StatusBar playerHealthBar = null;
    [SerializeField]
    private Transform enemyHealthBarsContainer = null;

    private List<StatusBar> enemyBars = new List<StatusBar>();

    public void InitHealthBars(PlayerControl player, int enemiesCount)
    {
        StatusValues playerValues = new StatusValues(player.name, 0, player.Health.TotalValue, player.Health.Value);
        playerHealthBar.Init(playerValues);
        player.Health.OnDamageTaken += playerHealthBar.Slider.SetValue;

        for (int i = 0; i < enemiesCount; ++i)
        {
            enemyBars.Add(Instantiate(statusBarPrefab, enemyHealthBarsContainer));
            enemyBars[i].Slider.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    public void ShowEnemyBars(List<EnemyControl> enemies)
    {
        enemyHealthBarsContainer.gameObject.SetActive(true);

        for (int i = 0; i < enemyBars.Count; ++i)
        {
            EnemyControl enemy = enemies[i];
            StatusValues values = new StatusValues(enemy.name, 0, enemy.Health.TotalValue, enemy.Health.Value);
            enemyBars[i].Init(values);
            enemy.Health.OnDamageTaken += enemyBars[i].Slider.SetValue;
        }
    }

    public void HideEnemyBars()
    {
        enemyHealthBarsContainer.gameObject.SetActive(false);
    }
}
