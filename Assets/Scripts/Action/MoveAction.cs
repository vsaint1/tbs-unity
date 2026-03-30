using System.Collections.Generic;
using UnityEngine;

public class MoveAction : BaseAction {


    private readonly float moveSpeed = 5f;
    private Vector3 targetPosition;

    [SerializeField]
    private Animator animator;

    [SerializeField]
    private int maxMoveDistance = 2;


    protected override void Awake() {
        base.Awake();
        targetPosition = transform.position;
    }


    void Start() {
    }

    void Update() {

        if (IsActive) {

            if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {

                Vector3 direction = (targetPosition - transform.position).normalized;
                if (direction != Vector3.zero) {
                    Quaternion targetRotation = Quaternion.LookRotation(direction);
                    const float rotateSpeed = 10f;
                    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
                }

                transform.position += moveSpeed * Time.deltaTime * direction;

                animator.SetBool("IsMoving", true);


            }
            else {
                animator.SetBool("IsMoving", false);
                EndAction();

            }

        }


    }

    public void Move(GridPosition gridPosition) {
        targetPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
        if (!IsValidActionGridPosition(gridPosition)) {
            Debug.LogError($"Unit: {Unit.name} tried to move to invalid position {gridPosition}");
            return;
        }

        StartAction();
    }

    public override void TakeAction(GridPosition gridPosition) {
        Move(gridPosition);
    }

    public bool IsValidActionGridPosition(GridPosition gridPosition) {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }

    public List<GridPosition> GetValidActionGridPositionList() {
        List<GridPosition> validGridPositionList = new List<GridPosition>();

        GridPosition unitGridPosition = Unit.GetGridPosition();

        for (int x = -maxMoveDistance; x <= maxMoveDistance; x++) {
            for (int z = -maxMoveDistance; z <= maxMoveDistance; z++) {
                GridPosition offsetGridPosition = new GridPosition(x, z);
                GridPosition testGridPosition = unitGridPosition + offsetGridPosition;

                int testDistance = Mathf.Abs(x) + Mathf.Abs(z);
                if (testDistance > maxMoveDistance) {
                    continue;
                }

                if (unitGridPosition == testGridPosition) {
                    continue;
                }

                if (!LevelGrid.Instance.IsValidGridPosition(testGridPosition)) {
                    continue;
                }

                if (LevelGrid.Instance.HasAnyUnitOnGridPosition(testGridPosition)) {
                    continue;
                }

                validGridPositionList.Add(testGridPosition);
            }

        }


        return validGridPositionList;
    }


    public override string GetActionName() {
        return "Move";
    }
}
