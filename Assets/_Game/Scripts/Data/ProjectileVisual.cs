using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileVisual : MonoBehaviour
{
    [SerializeField] private Transform projectileVisual;
    [SerializeField] private Transform projectileShadow;
    [SerializeField] private ProjectileSunny projectile;

    private Transform target;

    private Vector3 trajectoryStartPosition;



    private void Update()
    {
        UpdateProjectileRotation();
        //UpdateShadowPosition();
    }

    //private void UpdateShadowPosition()
    //{
    //   Vector3 trajectoryRange = target.position - trajectoryStartPosition;

    //    Vector3 newPosition = transform.position;
    //    newPosition.y = trajectoryStartPosition.y + projectile.GetNextYTratoryPosition() / + projectile.GetNextPositionYCorrectAbsolute();

    //    projectileShadow.position = newPosition;

    //}

    private void UpdateProjectileRotation()
    {
       Vector3 projectMoveDir = projectile.GetProjectileMoveDir();

        projectileVisual.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectMoveDir.y, projectMoveDir.x) * Mathf.Rad2Deg);
        projectileShadow.transform.rotation = Quaternion.Euler(0, 0, Mathf.Atan2(projectMoveDir.y, projectMoveDir.x) * Mathf.Rad2Deg);
    }

    public void SetTarget(Transform target) {
        this.target = target;
    }
}
