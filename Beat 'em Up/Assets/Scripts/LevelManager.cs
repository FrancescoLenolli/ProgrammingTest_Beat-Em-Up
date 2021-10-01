using System.Collections;
using System.Collections.Generic;
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
    [SerializeField]
    private Transform lowerLimit = null;
    [SerializeField]
    private Transform upperLimit = null;
    [SerializeField]
    private List<Transform> wavesPosition = new List<Transform>();

    private PlayerControl player;
    private int currentIndex;
    private int activeEnemiesCount;
    private bool canStartWave;

    private void Awake()
    {
        canStartWave = true;
        player = FindObjectOfType<PlayerControl>();
        currentIndex = 0;
        cameraController.SetUp(lowerLimit, upperLimit, player.transform);
    }

    private void Update()
    {
        if (currentIndex == wavesPosition.Count || !(player.transform.position.x >= wavesPosition[currentIndex].localPosition.x) || !canStartWave)
            return;

        StartWave();
    }

    public void EnemyDied()
    {
        activeEnemiesCount = Mathf.Clamp(--activeEnemiesCount, 0, activeEnemiesCount);
        if(activeEnemiesCount == 0)
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
        // Randomly decide if enemies should spawn on the left or right of the camera viewport
        float cameraLimit = Random.Range(0, 2) == 0 ? righPosition : leftPosition;
        // Add an offset to the right limit to spawn enemies outside the camera viewport.
        Vector3 startingPosition = new Vector3(cameraLimit + .5f, positionY, 0);

        EnemyControl newEnemy = Instantiate(prefabEnemy);
        newEnemy.Init(this, player, startingPosition);
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

}
