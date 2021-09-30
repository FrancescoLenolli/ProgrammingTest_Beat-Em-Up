using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField]
    private EnemyControl prefabEnemy = null;
    [Min(1)]
    [Tooltip("How many enemies get spawned in each wave")]
    [SerializeField]
    private int enemyCount = 1;
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
        canStartWave = false;
        cameraController.LockCamera(true);
        Vector3 cameraRightLimit = cameraController.GetRightLimit();

        for (int i = 0; i < enemyCount; ++i)
        {
            float positionY = cameraController.transform.position.y + Random.Range(-.3f, .3f);
            // Add an offset to the right limit to spawn enemies outside the camera viewport.
            Vector3 startingPosition = new Vector3(cameraRightLimit.x + .5f, positionY, 0);
            EnemyControl newEnemy = Instantiate(prefabEnemy);
            newEnemy.Init(this, player, startingPosition);
            ++activeEnemiesCount;
        }
    }
}
