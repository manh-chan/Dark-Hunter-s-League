using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ExplodeEnemyMovement : EnemyMovement
{
    public float distanceAttack = 0.5f;
    public float waitMoveSpeed = 0;

    private Color color1 = Color.white;
    private Color color2 = Color.red;
    private float duration = 0.2f;

    protected override void Start()
    {
        base.Start();
    }

    public override void Move()
    {
        base.Move();
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= distanceAttack)
        {
            rezoSpeedMove = false;
            StartCoroutine(ChangeColorLoop());
            Invoke(nameof(ExplodeEnemy), 1f);
        }
    }
    IEnumerator ChangeColorLoop()
    {
        while (true)
        {
            sorted.color = color2;
            yield return new WaitForSeconds(duration);
            sorted.color = color1;
            yield return new WaitForSeconds(duration);
        }
    }
    private void ExplodeEnemy()
    {
        Debug.Log("no");
    }
}
