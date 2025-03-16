using UnityEngine;

public class MeleeEnemyAttackState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log(enemy.enemyData.enemyName + " is ATTACKING.");
    }

    public override void UpdateState(EnemyStateMachine enemy)
    {
        if (Vector2.Distance(enemy.transform.position, enemy.player.position) < 1.5f)
        {
            enemy.player.GetComponent<Player>().TakeDamage(enemy.meleeDamage);
        }
        else
        {
            enemy.SwitchState(enemy.meleeChaseState);
        }
    }

    public override void ExitState(EnemyStateMachine enemy)
    {
        Debug.Log(enemy.enemyData.enemyName + " leaving ATTACK state.");
    }
}
