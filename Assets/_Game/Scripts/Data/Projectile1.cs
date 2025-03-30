using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile1 : MonoBehaviour
{
    private Transform target;
    private float moveSpeed;

    private float distanceToTargetToDestroyProjectile = 1f;

    private void Update()
    {
        Vector3 moveDirNormalLized = (target.position - transform.position).normalized;
        transform.position += moveDirNormalLized * moveSpeed;
        if (Vector3.Distance(transform.position, target.position) < distanceToTargetToDestroyProjectile) {
            Destroy(gameObject);
        }
    }
    public void InitializeProjectile(Transform target, float moveSpeed) { 
        this.target = target;
        this.moveSpeed = moveSpeed;
    }
}
