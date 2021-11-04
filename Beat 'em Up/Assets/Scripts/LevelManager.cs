using System;
using System.Collections;
using System.Collections.Generic;
using UIFramework.StateMachine;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private EnemyControl prefabEnemy = null;
    [Min(1)]
    [Tooltip("How many enemies get spawned in each wave.")]
    [SerializeField]
    private int enemyCount = 1;
    [Min(0)]
    [Tooltip("Enemies spawn each x seconds.")]
    [SerializeField]
    private float spawnDelay = .2f;
    [SerializeField]
    private CameraController cameraController = null;
    [Tooltip("On what positions can a Wave of enemies start.")]
    [SerializeField]
    private List<Transform> wavesPosition = new List<Transform>();
    [SerializeField]
    private UIStateMachine hudStateMachine = null;

    private PlayerControl player;
    private Vector3 startingPosition;
    private int currentIndex;
    private int activeEnemiesCount;
    private bool canStartWave;
    private bool canContinueGame;
    private bool levelEnded;
    private bool levelCompleted;
    private List<EnemyControl> enemies = new List<EnemyControl>();
    private Action<List<EnemyControl>> onStartWave;
    private Action onEndWave;
    private Action onLevelReset;
    private Action<bool> onLevelCompleted;

    public Action<List<EnemyControl>> OnStartWave { get => onStartWave; set => onStartWave = value; }
    public Action OnEndWave { get => onEndWave; set => onEndWave = value; }
    public Action<bool> OnLevelCompleted { get => onLevelCompleted; set => onLevelCompleted = value; }
    public int EnemyCount { get => enemyCount; }
    public Action OnLevelReset { get => onLevelReset; set => onLevelReset = value; }

    private void Start()
    {
        player = FindObjectOfType<PlayerControl>();
        startingPosition = player.transform.position;
        canStartWave = true;
        canContinueGame = true;
        levelEnded = false;
        levelCompleted = false;
        currentIndex = 0;
        cameraController.SetUp(player.transform);
        hudStateMachine.FirstStart();
    }

    private void Update()
    {
        if (!player)
            return;

        LevelEndedCheck();

        bool canStartNextWave = currentIndex != wavesPosition.Count && player.transform.position.x >= wavesPosition[currentIndex].localPosition.x && canStartWave;
        if (canStartNextWave)
            StartCoroutine(StartWaveRoutine());
    }

    public void EnemyDied()
    {
        activeEnemiesCount = Mathf.Clamp(--activeEnemiesCount, 0, activeEnemiesCount);
        bool allEnemiesDied = activeEnemiesCount == 0;
        if (allEnemiesDied)
        {
            EndWave();
        }
    }

    public void ContinueGame()
    {
        /*
         * If more levels are added, change Scene when
         * the current level is completed instead of restarting it.
         */

        if (!levelCompleted)
            StartCoroutine(ResetLevelRoutine());
        else
            StartCoroutine(ResetLevelRoutine());
    }

    /// <summary>
    /// Check if the level has ended, and what happens after that.
    /// </summary>
    private void LevelEndedCheck()
    {
        if (canContinueGame && !levelEnded)
        {
            bool playerDead = !player.IsAlive;
            bool currentLevelCompleted = player.IsAlive && currentIndex >= wavesPosition.Count;

            if (playerDead)
            {
                levelEnded = true;
                levelCompleted = false;
                onLevelCompleted?.Invoke(levelCompleted);
            }
            else if (currentLevelCompleted)
            {
                levelEnded = true;
                levelCompleted = true;
                onLevelCompleted?.Invoke(levelCompleted);
            }
        }
    }

    private EnemyControl SpawnEnemy(float leftPosition, float righPosition)
    {
        float positionY = cameraController.transform.position.y + UnityEngine.Random.Range(-.2f, .2f);
        float positionOffset = .5f;

        // Randomly decide if enemies should spawn on the left or right of the camera viewport.
        // Add an offset to spawn them outside the camera viewport.
        float cameraLimit = UnityEngine.Random.Range(0, 2) == 0 ? righPosition + positionOffset : leftPosition - positionOffset;
        Vector3 startingPosition = new Vector3(cameraLimit, positionY, 0);

        EnemyControl newEnemy = Instantiate(prefabEnemy);
        newEnemy.name = "Enemy";
        newEnemy.Init(this, player, startingPosition);
        ++activeEnemiesCount;

        return newEnemy;
    }

    private void EndWave()
    {
        currentIndex = Mathf.Clamp(++currentIndex, 0, wavesPosition.Count);
        if (currentIndex != wavesPosition.Count)
        {
            cameraController.LockCamera(false);
            canStartWave = true;
        }

        onEndWave?.Invoke();
    }

    private IEnumerator StartWaveRoutine()
    {
        canStartWave = false;
        cameraController.LockCamera(true);
        float cameraRightLimitX = cameraController.GetRightLimit().x;
        float cameraLeftLimitX = cameraController.GetLeftLimit().x;
        List<EnemyControl> newEnemies = new List<EnemyControl>();

        for (int i = 0; i < enemyCount; ++i)
        {
            EnemyControl enemy = SpawnEnemy(cameraLeftLimitX, cameraRightLimitX);
            enemies.Add(enemy);
            newEnemies.Add(enemy);
            yield return new WaitForSeconds(spawnDelay);
        }

        onStartWave?.Invoke(newEnemies);

        yield return null;
    }

    private IEnumerator ResetLevelRoutine()
    {
        canContinueGame = false;
        canStartWave = false;

        enemies.ForEach(enemy => Destroy(enemy.gameObject));
        enemies.Clear();
        currentIndex = 0;
        activeEnemiesCount = 0;
        cameraController.LockCamera(false);
        player.Restart(startingPosition);
        onLevelReset?.Invoke();

        canStartWave = true;
        canContinueGame = true;
        levelEnded = false;

        yield return null;
    }

}
