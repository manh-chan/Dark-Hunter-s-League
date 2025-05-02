using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;

public class SpawnManager : MonoBehaviour
{
    int currentWaveIndex;
    int currentWaveSpawnCount = 0;

    public WaveData[] data;
    public Camera referenceCamera;

    [Tooltip("If there are more than this number of enemies, stop spawning any more. For performance.")]
    public int maximumEnemyCount = 300;

    float spawnTimer;
    float currentWaveDuration = 0f;
    public bool boostedByCurse = true;
    public static SpawnManager instance;

    public TMP_Text winGame;

    public PlayerStats playerStats;
    private void Start()
    {
        if (instance)
            Debug.LogWarning("There is more than one Spawn Manager in the scene! Please remove the extras.");

        instance = this;


        if (!referenceCamera)
            referenceCamera = Camera.main;
    }

    private void Update()
    {
        if (GameManager.instance.loadGameEnd == false) return;

        spawnTimer -= Time.deltaTime;
        currentWaveDuration += Time.deltaTime;

        if (spawnTimer <= 0)
        {
            if (HasWaveEnded())
            {
                currentWaveIndex++;
                currentWaveDuration = currentWaveSpawnCount = 0;

                if (currentWaveIndex >= data.Length)
                {
                    Debug.Log("All waves have been spawned! Shutting down.", this);
                    if (!GameManager.instance.isGameOver)
                    {
                        Invoke(nameof(WinGame),3);
                    }
                    enabled = false;
                }
                return;
            }

            if (!CanSpawn())
            {
                ActivateCooldown();
                return;
            }

            GameObject[] spawns = data[currentWaveIndex].GetSpawns(EnemyState.count);

            foreach (GameObject prefab in spawns)
            {
                if (!CanSpawn()) continue;

                Instantiate(prefab, GeneratePosition(), Quaternion.identity);
                currentWaveSpawnCount++;
            }

            ActivateCooldown();
        }
    }
    public void ActivateCooldown()
    {
        float curseBoost = boostedByCurse ? GameManager.GetCumulativeCurse() : 1;
        spawnTimer += data[currentWaveIndex].GetSpawnInterval() / curseBoost;
    }
    public bool CanSpawn()
    {
        if (HasExceededMaxEnemies()) return false;
        if (currentWaveSpawnCount > data[currentWaveIndex].totalSpawn) return false;
        if (currentWaveDuration > data[currentWaveIndex].duration) return false;
        return true;
    }
    public static bool HasExceededMaxEnemies()
    {
        if (!instance) return false;
        if (EnemyState.count > instance.maximumEnemyCount) return true;
        return false;
    }
    private void WinGame()
    {
        winGame.gameObject.SetActive(true);
        GameManager.instance.GameOver();
    }
    public bool HasWaveEnded()
    {
        WaveData currentWave = data[currentWaveIndex];

        if ((currentWave.exitConditions & WaveData.ExitCondition.waveDuration) > 0)
            if (currentWaveDuration < currentWave.duration) return false;

        if ((currentWave.exitConditions & WaveData.ExitCondition.reachedTotalSpawns) > 0)
            if (currentWaveSpawnCount < currentWave.totalSpawn) return false;

        if (currentWave.mustKillAll && EnemyState.count > 0) return false;

        return true;
    }

    private void Reset()
    {
        referenceCamera = Camera.main;
    }

    public static Vector3 GeneratePosition()
    {
        if (!instance.referenceCamera) instance.referenceCamera = Camera.main;

        if (!instance.referenceCamera.orthographic)
            Debug.LogWarning("The reference camera is not orthographic! This will cause enemy spawns to sometimes appear outside the camera boundaries.");

        float x = Random.Range(0f, 1f);
        float y = Random.Range(0f, 1f);

        switch (Random.Range(0, 2))
        {
            case 0:
            default:
                return instance.referenceCamera.ViewportToWorldPoint(new Vector3(Mathf.Round(x), y));
            case 1:
                return instance.referenceCamera.ViewportToWorldPoint(new Vector3(x, Mathf.Round(y)));
        }
    }

    public static bool IsWithinBoundaries(Transform checkedObject)
    {
        Camera c = instance && instance.referenceCamera ? instance.referenceCamera : Camera.main;

        Vector2 viewport = c.WorldToViewportPoint(checkedObject.position);
        if (viewport.x < 0f || viewport.x > 1f) return false;
        if (viewport.y < 0f || viewport.y > 1f) return false;
        return true;
    }
}
