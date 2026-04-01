

using System;
using System.Collections.Generic;
using UnityEngine;

public class ShootAction : BaseAction {

    enum State {
        Aiming,
        Shooting,
        EndingAction
    };

    private int maxShootDistance = 7;

    private float stateTimer;

    private State state;

    private Unit targetUnit;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private GameObject bulletProjectilePrefab;

    [SerializeField]
    private Transform shootPointTransform;


    void Start() {
    }
    void Update() {
        if (!IsActive) return;

        switch (state) {
            case State.Aiming:
                if (targetUnit != null)
                    LookAtTarget();

                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f) {
                    state = State.Shooting;
                    stateTimer = 0.1f;
                }
                break;

            case State.Shooting:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f) {
                    if (targetUnit != null)
                        Shoot();

                    state = State.EndingAction;
                    stateTimer = 0.1f;
                }
                break;

            case State.EndingAction:
                stateTimer -= Time.deltaTime;
                if (stateTimer <= 0f)
                    EndAction();
                break;
        }
    }

    void SpawnShootVFX() {

        GameObject bulletObject = Instantiate(bulletProjectilePrefab, shootPointTransform.position, Quaternion.identity);

        BulletProjectile bulletProjectile = bulletObject.GetComponent<BulletProjectile>();

        bulletProjectile.SetTarget(targetUnit.transform.position + Vector3.up * 1.5f);


    }

    void LookAtTarget() {

        Vector3 aimDirection = (targetUnit.transform.position - Unit.transform.position).normalized;
        Quaternion targetRotation = Quaternion.LookRotation(aimDirection);

        const float rotateSpeed = 5f;
        Unit.transform.rotation = Quaternion.Slerp(
             Unit.transform.rotation,
             targetRotation,
             rotateSpeed * Time.deltaTime
         );
    }

    public override List<GridPosition> GetValidActionGridPositionList() {

        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = Unit.GetGridPosition();

        for (int x = -maxShootDistance; x <= maxShootDistance; x++) {
            for (int z = -maxShootDistance; z <= maxShootDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxShootDistance) {
                    continue;
                }

                // validGridPositionList.Add(testGridPosition);
                // continue;

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) {
                    continue;
                }

                if (!LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) {
                    continue;
                }

                Unit target = LevelGrid.Instance.GetUnitAtGridPosition(testGridPosition, 0);
                if (target == null || target.IsEnemy() == Unit.IsEnemy()) {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }

        }


        return validGridPositionList;
    }


    void Shoot() {
        animator.SetTrigger("Shoot");
        SpawnShootVFX();
        targetUnit.TakeDamage(100);

    }

    public override void TakeAction(GridPosition gridPosition) {
        targetUnit = LevelGrid.Instance.GetUnitAtGridPosition(gridPosition, 0);
        if (targetUnit != null) {
            state = State.Aiming;
            stateTimer = 1f;
            StartAction();
        }
        else {
            Debug.LogError($"No target unit found at {gridPosition} to shoot!");
        }

    }

    public override string GetActionName() {
        return "Shoot";
    }
    public override int GetActionPointCost() {
        return 2;
    }
}