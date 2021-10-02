﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
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
    [Tooltip("Set to TRUE to wait on player input to restart level.\nIf set to FALSE, use the 'Restart Delay' property to set the restart wait time.")]
    [SerializeField]
    private bool restartOnPlayerInput = false;
    [Min(0)]
    [SerializeField]
    private float restartDelay = .5f;
    [SerializeField]
    private CameraController cameraController = null;
    [Tooltip("Bottom left of the current Level.")]
    [SerializeField]
    private Transform lowerLimit = null;
    [Tooltip("Top right of the current Level.")]
    [SerializeField]
    private Transform upperLimit = null;
    [Tooltip("On what positions can a Wave of enemies start.")]
    [SerializeField]
    private List<Transform> wavesPosition = new List<Transform>();

    private PlayerControl player;
    private Vector3 startingPosition;
    private int currentIndex;
    private int activeEnemiesCount;
    private bool canStartWave;
    private bool canRestartLevel;
    private List<EnemyControl> enemies = new List<EnemyControl>();

    private void Awake()
    {
        player = FindObjectOfType<PlayerControl>();
        startingPosition = player.transform.position;
        canStartWave = true;
        canRestartLevel = true;
        currentIndex = 0;
        cameraController.SetUp(lowerLimit, upperLimit, player.transform);
    }

    private void Update()
    {
        if (!player)
            return;

        if (!player.IsAlive && canRestartLevel)
            StartCoroutine(ResetLevelRoutine());

        if (currentIndex == wavesPosition.Count || !(player.transform.position.x >= wavesPosition[currentIndex].localPosition.x) || !canStartWave)
            return;

        StartWave();
    }

    public void EnemyDied()
    {
        activeEnemiesCount = Mathf.Clamp(--activeEnemiesCount, 0, activeEnemiesCount);
        if (activeEnemiesCount == 0)
        {
            currentIndex = Mathf.Clamp(++currentIndex, 0, wavesPosition.Count);
            cameraController.LockCamera(false);
            canStartWave = true;
        }
    }

    private void StartWave()
    {
        StartCoroutine(StartWaveRoutine());
    }

    private void SpawnEnemy(float leftPosition, float righPosition)
    {
        float positionY = cameraController.transform.position.y + Random.Range(-.2f, .2f);
        float positionOffset = .5f;

        // Randomly decide if enemies should spawn on the left or right of the camera viewport.
        // Add an offset to spawn them outside the camera viewport.
        float cameraLimit = Random.Range(0, 2) == 0 ? righPosition + positionOffset : leftPosition - positionOffset;
        Vector3 startingPosition = new Vector3(cameraLimit, positionY, 0);

        EnemyControl newEnemy = Instantiate(prefabEnemy);
        newEnemy.Init(this, player, startingPosition);
        enemies.Add(newEnemy);
        ++activeEnemiesCount;
    }

    private IEnumerator StartWaveRoutine()
    {
        canStartWave = false;
        cameraController.LockCamera(true);
        float cameraRightLimitX = cameraController.GetRightLimit().x;
        float cameraLeftLimitX = cameraController.GetLeftLimit().x;

        for (int i = 0; i < enemyCount; ++i)
        {
            SpawnEnemy(cameraLeftLimitX, cameraRightLimitX);
            yield return new WaitForSeconds(spawnDelay);
        }

        yield return null;
    }

    private IEnumerator ResetLevelRoutine()
    {
        canRestartLevel = false;
        canStartWave = false;

        if (restartOnPlayerInput)
            while (!Input.GetKeyDown(KeyCode.R))
                yield return null;
        else
            yield return new WaitForSeconds(restartDelay);

        enemies.ForEach(enemy => Destroy(enemy.gameObject));
        enemies.Clear();
        currentIndex = 0;
        activeEnemiesCount = 0;
        cameraController.LockCamera(false);
        player.Restart(startingPosition);
        canStartWave = true;
        canRestartLevel = true;

        yield return null;
    }

}
