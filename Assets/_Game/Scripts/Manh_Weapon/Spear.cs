using UnityEngine;
using System.Collections;

public class Spear : Weapon
{
    public float attackRange = 5f;  // Phạm vi tìm kẻ địch
    public float thrustDistance = 1.5f;  // Khoảng cách đâm
    public float thrustSpeed = 0.2f;  // Tốc độ đâm
    public float returnSpeed = 0.2f;  // Tốc độ rút
    public float attackCooldown = 1.5f;  // Thời gian giữa các đòn tấn công

    protected override void OnInit()
    {
        base.OnInit();
        initialPosition = TF.localPosition;
        hitCollider.enabled = false;
        StartCoroutine(AutoAttack());
    }

    private IEnumerator AutoAttack()
    {
        while (true)
        {
            yield return new WaitForSeconds(attackCooldown);
            nearestEnemy = FindNearestEnemy(attackRange);
            if (nearestEnemy != null)
            {
                Vector3 direction = (nearestEnemy.position - TF.position).normalized;
                yield return StartCoroutine(EnventAttack(direction));
            }
        }
    }
    private IEnumerator EnventAttack(Vector3 direction)
    {
        isAttacking = true;
        Vector3 targetPosition = initialPosition + direction * thrustDistance;
        // Đâm ra
        hitCollider.enabled = true;
        float elapsedTime = 0;
        while (elapsedTime < thrustSpeed)
        {
            TF.localPosition = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / thrustSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        TF.localPosition = targetPosition;

        yield return new WaitForSeconds(0.1f);

        // Rút về
        hitCollider.enabled = false;
        elapsedTime = 0;
        while (elapsedTime < returnSpeed)
        {
            TF.localPosition = Vector3.Lerp(targetPosition, initialPosition, elapsedTime / returnSpeed);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        TF.localPosition = initialPosition;
        isAttacking = false;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(TF.position, attackRange);
    }
}
