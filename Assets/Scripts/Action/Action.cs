using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAction : MonoBehaviour {

    protected Unit Unit { get; private set; }
    protected bool IsActive { get; private set; }

    public void StartAction() {
        UnitActionSystem.Instance.SetBusy();
        IsActive = true;
    }

    public void EndAction() {
        UnitActionSystem.Instance.ClearBusy();
        IsActive = false;
    }

    protected virtual void Awake() {
        Unit = GetComponent<Unit>();
    }


    public virtual int GetActionPointCost() {
        return 1;
    }

    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition);

    public abstract List<GridPosition> GetValidActionGridPositionList();

    public virtual bool IsValidActionGridPosition(GridPosition gridPosition) {
        List<GridPosition> validGridPositionList = GetValidActionGridPositionList();
        return validGridPositionList.Contains(gridPosition);
    }
}

