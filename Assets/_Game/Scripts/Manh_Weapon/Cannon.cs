using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : Weapon
{
    public GameObject bulletPrefab; // Prefab đạn
    public Transform firePoint; // Vị trí bắn đạn
    public float fireRate = 1f; // Tốc độ bắn (đạn/s)
    public float bulletSpeed = 10f; // Tốc độ đạn
    public float attackRange = 5f; // Phạm vi tìm kẻ địch

    private float fireCooldown = 0f;

    protected override void Attack()
    {
        base.Attack();
        nearestEnemy = FindNearestEnemy(attackRange);
        // Nếu đến thời gian bắn -> bắn
        if (nearestEnemy != null && fireCooldown <= 0f)
        {
            Shoot();
            fireCooldown = 1f / fireRate;
        }
        fireCooldown -= Time.deltaTime;
    }
    private void Shoot()
    {
        if (bulletPrefab == null || firePoint == null) return;

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        if (rb == null) return;
            rb.velocity = firePoint.right * bulletSpeed;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
