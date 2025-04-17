using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSunny : WeaponEffect
{
    private Transform target;
    [SerializeField] private AnimationCurve trajectoryAnimationCurve;
    [SerializeField] private AnimationCurve axisCorrectionAnimtionCurve;
    [SerializeField] private AnimationCurve projectileSpeedAnimtionCurve;

    private Vector3 trajectoryStartPoint;
    private Vector3 projectileMoveDir;
    private Vector3 trajectoryRange;

    [SerializeField] private float trajectoryMaxHeight =3f;
    [SerializeField] private float moveSpeed = 0.3f;
    [SerializeField] private float maxMoveSpeed = 0.3f;
    private float distanceToTargetToDestroyProjectile = 1f;
    private float nextYTrajectoryPosition;
    private float nextXTrajectoryPosition;
    private float nextPositionYCorrectionAbsolute;
    private float nextPositionXCorrectionAbsolute;

    private void Start()
    {
        trajectoryStartPoint = transform.position;
    }
    private void Update()
    {
        UpdateProjectilePosition();

        if (Vector3.Distance(transform.position, target.position) < distanceToTargetToDestroyProjectile) {
            Destroy(gameObject);
        }
    }

    private void UpdateProjectilePosition() {
        trajectoryRange = target.position - trajectoryStartPoint;
        if (Mathf.Abs(trajectoryRange.normalized.x) < Mathf.Abs(trajectoryRange.normalized.y))
        {
            if (trajectoryRange.y < 0)
            {
                moveSpeed = -moveSpeed;

            }
            UpdatePositionWithXCurve();
        }
        else {
            if (trajectoryRange.x < 0)
            {

                moveSpeed = -moveSpeed;

            }
            UpdatePositionWithYCurve();
        }
    }

    private void UpdatePositionWithXCurve()
    {
        float nextPositionY = transform.position.y + moveSpeed * Time.deltaTime;
        float nextPositionYNormalized = (nextPositionY - trajectoryStartPoint.y) / trajectoryRange.y;

        float nextPositionXNormalLized = trajectoryAnimationCurve.Evaluate(nextPositionYNormalized);
        nextXTrajectoryPosition = nextPositionXNormalLized * trajectoryMaxHeight;

        float nextPositionXCorrectionNormalLized = axisCorrectionAnimtionCurve.Evaluate(nextPositionYNormalized);
        nextPositionXCorrectionAbsolute = nextPositionXCorrectionNormalLized * trajectoryRange.x;

        float nextPositionX = trajectoryStartPoint.x + nextXTrajectoryPosition + nextPositionXCorrectionAbsolute;

        Vector3 newPostion = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateNextProjectileSpeed(nextPositionYNormalized);

        projectileMoveDir = newPostion - transform.position;

        transform.position = newPostion;
    }

    private void UpdatePositionWithYCurve() {

        float nextPositionX = transform.position.x + moveSpeed * Time.deltaTime;
        float nextPositionXNormalized = (nextPositionX - trajectoryStartPoint.x) / trajectoryRange.x;

        float nextPositionYNormalLized = trajectoryAnimationCurve.Evaluate(nextPositionXNormalized);
        nextYTrajectoryPosition = nextPositionYNormalLized * trajectoryMaxHeight;

        float nextPositionYCorrectionNormalLized = axisCorrectionAnimtionCurve.Evaluate(nextPositionXNormalized);
        nextPositionYCorrectionAbsolute = nextPositionYCorrectionNormalLized * trajectoryRange.y;

        float nextPositionY = trajectoryStartPoint.y + nextYTrajectoryPosition + nextPositionYCorrectionAbsolute;

        Vector3 newPostion = new Vector3(nextPositionX, nextPositionY, 0);

        CalculateNextProjectileSpeed(nextPositionXNormalized);

        projectileMoveDir = newPostion - transform.position;

        transform.position = newPostion;


    }

    private void CalculateNextProjectileSpeed(float nextPositionXNormalized) { 
        float nextMoveSpeedNormalLize = projectileSpeedAnimtionCurve.Evaluate(nextPositionXNormalized);

        moveSpeed = nextMoveSpeedNormalLize * maxMoveSpeed;
    }
    public Vector3 GetProjectileMoveDir() {
        return projectileMoveDir;
    }
    public void InitializeProjectile(Transform target, float maxMoveSpeed, float trajectoryMaxHeight) { 
        this.target = target;
        this.maxMoveSpeed = maxMoveSpeed;

        float xDistanceToTaget = target.position.x  - transform.position.x;

        this.trajectoryMaxHeight = Mathf.Abs(xDistanceToTaget) * trajectoryMaxHeight;
    }

    public void InitializeAnimationCurve(AnimationCurve trajectoryAnimationCurve, AnimationCurve axisCorrectionAnimtionCurve, AnimationCurve projectileSpeedAnimtionCurve)
    {
        this.trajectoryAnimationCurve = trajectoryAnimationCurve;
        this.axisCorrectionAnimtionCurve = axisCorrectionAnimtionCurve;
        this.projectileSpeedAnimtionCurve = projectileSpeedAnimtionCurve;
    }

    public float GetNextYTratoryPosition() { 
     return nextYTrajectoryPosition;
    }

    public float GetNextPositionYCorrectAbsolute() { 
        return nextPositionYCorrectionAbsolute;
    }
}
