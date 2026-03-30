using UnityEngine;

public class Unit : MonoBehaviour {



    private GridPosition gridPosition;

    private MoveAction moveAction;
    private SpinAction spinAction;

    private BaseAction[] actions;


    void Start() {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        actions = GetComponents<BaseAction>();

        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);
    }

    void Update() {


        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (!newGridPosition.Equals(gridPosition)) {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }


    public MoveAction GetMoveAction() {
        return moveAction;
    }

    public SpinAction GetSpinAction() {
        return spinAction;
    }

    public BaseAction[] GetActions() {
        return actions;
    }

    public GridPosition GetGridPosition() {
        return gridPosition;
    }

}
