using UnityEngine;
using System.Collections;

public class RangedEnemyAttackState : EnemyBaseState
{
    private float lastAttackTime = 0f;
    private Vector2 targetPosition; // Điểm đến tiếp theo của enemy
    private float changeTargetCooldown = 3f; // Thời gian trước khi chọn điểm mới
    private float lastTargetChangeTime = 0f;

    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log(enemy.enemyData.enemyName + " is ATTACKING.");
        ChooseNewTargetPosition(enemy);
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.player == null) return;

        // Di chuyển đến vị trí mới
        enemy.transform.position = Vector2.MoveTowards(
            enemy.transform.position,
            targetPosition,
            enemy.moveSpeed * Time.deltaTime
        );

        // Nếu đã đến gần vị trí mục tiêu, chọn một điểm mới
        if (Vector2.Distance(enemy.transform.position, targetPosition) < 0.5f ||
            Time.time > lastTargetChangeTime + changeTargetCooldown)
        {
            ChooseNewTargetPosition(enemy);
        }

        // Nếu đủ thời gian, bắn đạn
        if (Time.time > lastAttackTime + enemy.attackCooldown)
        {
            lastAttackTime = Time.time;
            ShootBullet(enemy);
        }
    }

    private void ChooseNewTargetPosition(EnemyStateMachine enemy)
    {
        // Chọn ngẫu nhiên một điểm xung quanh Player trong khoảng cách tấn công
        Vector2 randomDirection = Random.insideUnitCircle.normalized;
        targetPosition = (Vector2)enemy.player.position + randomDirection * enemy.attackRange;

        lastTargetChangeTime = Time.time;
    }

    private void ShootBullet(EnemyStateMachine enemy)
    {
        GameObject bullet = GameObject.Instantiate(enemy.bulletPrefab, enemy.firePoint.position, Quaternion.identity);
        bullet.GetComponent<EnemyBullet>().Initialize(enemy.player.position);
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        Debug.Log(enemy.enemyData.enemyName + " leaving ATTACK state.");
    }
}
