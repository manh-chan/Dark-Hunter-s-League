using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Mob Event Data", menuName = "Dark-Hunter-s-League/Event Data/Mob")]
public class MobEventData : EventData
{
    [Header("Mob Data")]
    public ParticleSystem spawnEffectPrefab;
    [Range(0f, 360f)] public float possibleAngles = 360f;
    [Min(0)] public float spawnRadius = 2f, spawnDistance = 20f;

    public override bool Activate(PlayerStats player = null)
    {
        if (player)
        {
            float randomAngle = Random.Range(0, possibleAngles) * Mathf.Deg2Rad;
            foreach (GameObject o in GetSpawns())
            {
                if (spawnEffectPrefab)
                {
                    Instantiate(spawnEffectPrefab, player.transform.position + new Vector3(
                        (spawnDistance + Random.Range(-spawnRadius, spawnRadius)) * Mathf.Cos(randomAngle),
                        (spawnDistance + Random.Range(-spawnRadius, spawnRadius)) * Mathf.Sin(randomAngle)
                        ), Quaternion.identity);
                }
                Instantiate(o, player.transform.position + new Vector3(
                    (spawnDistance + Random.Range(-spawnRadius, spawnRadius)) * Mathf.Cos(randomAngle),
                    (spawnDistance + Random.Range(-spawnRadius, spawnRadius)) * Mathf.Sin(randomAngle)
                    ), Quaternion.identity);
            }
        }
        return false;
    }
}
