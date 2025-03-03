using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spear : WeaponEffect
{
    //public float attackRange = 5f;  // Phạm vi tìm kẻ địch
    //public float thrustDistance = 1.5f;  // Khoảng cách đâm
    //public float thrustSpeed = 0.2f;  // Tốc độ đâm
    //public float returnSpeed = 0.2f;  // Tốc độ rút
    //public float attackCooldown = 1.5f;  // Thời gian giữa các đòn tấn công
    //public LayerMask enemyLayer;  // Lớp kẻ địch
    //public Collider2D hitCollider;  // Collider để gây sát thương

    //private Vector3 initialPosition;
    //private bool isAttacking = false;
    //private Transform targetEnemy;

    //void Start()
    //{
    //    initialPosition = transform.localPosition;
    //    hitCollider.enabled = false;
    //    StartCoroutine(AutoAttack());
    //}

    //void Update()
    //{
    //    targetEnemy = FindNearestEnemy();
    //    if (targetEnemy != null)
    //    {
    //        RotateTowardsEnemy(targetEnemy);
    //    }
    //}

    //private IEnumerator AutoAttack()
    //{
    //    while (true)
    //    {
    //        yield return new WaitForSeconds(attackCooldown);

    //        if (targetEnemy != null)
    //        {
    //            Vector3 direction = (targetEnemy.position - transform.position).normalized;
    //            yield return StartCoroutine(Thrust(direction));
    //        }
    //    }
    //}

    //private Transform FindNearestEnemy()
    //{
    //    Collider2D[] enemies = Physics2D.OverlapCircleAll(transform.position, attackRange, enemyLayer);
    //    Transform nearestEnemy = null;
    //    float minDistance = Mathf.Infinity;

    //    foreach (var enemy in enemies)
    //    {
    //        float distance = Vector2.Distance(transform.position, enemy.transform.position);
    //        if (distance < minDistance)
    //        {
    //            minDistance = distance;
    //            nearestEnemy = enemy.transform;
    //        }
    //    }

    //    return nearestEnemy;
    //}

    //private void RotateTowardsEnemy(Transform enemy)
    //{
    //    Vector3 direction = (enemy.position - transform.position).normalized;
    //    float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
    //    transform.rotation = Quaternion.Euler(0, 0, angle);
    //}

    //private IEnumerator Thrust(Vector3 direction)
    //{
    //    isAttacking = true;
    //    Vector3 targetPosition = initialPosition + direction * thrustDistance;

    //    // Đâm ra
    //    hitCollider.enabled = true;
    //    float elapsedTime = 0;
    //    while (elapsedTime < thrustSpeed)
    //    {
    //        transform.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / thrustSpeed);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.localPosition = targetPosition;

    //    yield return new WaitForSeconds(0.1f);

    //    // Rút về
    //    hitCollider.enabled = false;
    //    elapsedTime = 0;
    //    while (elapsedTime < returnSpeed)
    //    {
    //        transform.localPosition = Vector3.Lerp(targetPosition, initialPosition, elapsedTime / returnSpeed);
    //        elapsedTime += Time.deltaTime;
    //        yield return null;
    //    }
    //    transform.localPosition = initialPosition;
    //    isAttacking = false;
    //}

    //void OnDrawGizmosSelected()
    //{
    //    Gizmos.color = Color.red;
    //    Gizmos.DrawWireSphere(transform.position, attackRange);
    //}
}
