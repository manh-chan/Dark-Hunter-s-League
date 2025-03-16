using UnityEngine;

public class EnemyDeadState : EnemyBaseState
{
    public override void EnterState(EnemyStateMachine enemy)
    {
        Debug.Log("Enemy is DEAD.");
        GameObject.Destroy(enemy.gameObject);
    }

    public override void UpdateState(EnemyStateMachine enemy) { }
    public override void ExitState(EnemyStateMachine enemy) { }
}
