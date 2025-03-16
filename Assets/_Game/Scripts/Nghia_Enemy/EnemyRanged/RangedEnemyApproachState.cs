using UnityEngine;

public class RangedEnemyApproachState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Ranged Enemy is APPROACHING.");
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.player == null) return;

        // Di chuyển đến khoảng cách phù hợp
        float distance = Vector2.Distance(enemy.transform.position, enemy.player.position);
        if (distance > enemy.attackRange)
        {
            enemy.transform.position = Vector2.MoveTowards(
                enemy.transform.position,
                enemy.player.position,
                enemy.moveSpeed * Time.deltaTime
            );
        }
        else
        {
            enemy.SwitchState(enemy.rangedAttackState);
        }
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        Debug.Log("Ranged Enemy leaving APPROACH state.");
    }
}
