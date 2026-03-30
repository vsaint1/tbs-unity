


using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpinAction : BaseAction {
    private readonly float rotationSpeed = 360f;
    private float totalRotation = 0f;

    protected override void Awake() {
        base.Awake();
    }

    void Update() {
        if (IsActive) {
            float rotationThisFrame = rotationSpeed * Time.deltaTime;
            transform.eulerAngles += new Vector3(0f, rotationThisFrame, 0f);
            totalRotation += rotationThisFrame;

            if (totalRotation >= 360f) {
                EndAction();
                totalRotation = 0f;
            }
        }
    }

    public void Spin() {
        if (!IsActive) {
            StartAction();
        }
    }

    public override void TakeAction(GridPosition gridPosition) {
        Spin();
    }

    public override string GetActionName() {
        return "Spin";
    }

    public override int GetActionPointCost() {
        return 2;
    }

    public override List<GridPosition> GetValidActionGridPositionList() {
        return new List<GridPosition>() { Unit.GetGridPosition() };
    }
}