using UnityEngine;

public abstract class BaseAction : MonoBehaviour {

    protected Unit Unit { get; private set; }
    protected bool IsActive { get; private set; }


    public void StartAction() {
        IsActive = true;
    }

    public void EndAction() {
        IsActive = false;
    }

    protected virtual void Awake() {
        Unit = GetComponent<Unit>();
    }


    public abstract string GetActionName();

    public abstract void TakeAction(GridPosition gridPosition);
}

