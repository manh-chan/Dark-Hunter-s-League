using UnityEngine;

public class MeleeEnemyChaseState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Melee Enemy is now CHASING.");
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (enemy.player == null) return;

        // Enemy cận chiến dí theo Player
        enemy.transform.position = Vector2.MoveTowards(
            enemy.transform.position,
            enemy.player.position,
            enemy.moveSpeed * Time.deltaTime
        );

        // Khi chạm vào Player, chuyển sang trạng thái tấn công
        if (Vector2.Distance(enemy.transform.position, enemy.player.position) < 1f)
        {
            enemy.SwitchState(enemy.meleeAttackState);
        }
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        Debug.Log("Melee Enemy leaving CHASE state.");
    }
}
