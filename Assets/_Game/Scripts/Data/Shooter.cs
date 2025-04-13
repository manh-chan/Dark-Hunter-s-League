using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform target;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private AnimationCurve axisCorrectionAnimtionCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimtionCurve;

    [SerializeField] private float shootRate;
    [SerializeField] private float projectileMaxMoveSpeed;
    [SerializeField] private float projectileRelativeHeight;
    private float shootTimer;

    private void Update()
    {
        shootTimer -= Time.deltaTime;
        if (shootTimer <= 0)
        {
            shootTimer = shootRate;

            float spacing = 1f; // Khoảng cách giữa các viên đạn theo trục Y
            for (int i = -1; i <= 1; i++)
            {
                Vector3 spawnPosition = transform.position + new Vector3(0, i * spacing, 0); // Điều chỉnh theo trục Y
                ProjectileSunny projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity).GetComponent<ProjectileSunny>();

                projectile.InitializeProjectile(target, projectileMaxMoveSpeed, projectileRelativeHeight);
                projectile.InitializeAnimationCurve(trajectoryAnimationCurve, axisCorrectionAnimtionCurve, projectileSpeedAnimtionCurve);
            }
        }
    }
}
