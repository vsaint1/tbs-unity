using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UnitActionSystem : MonoBehaviour {


    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;

    public event EventHandler OnActionStarted;

    private Unit selectedUnit;

    [SerializeField]
    private LayerMask unitLayerMask;

    private BaseAction selectedAction;
    private bool isBusy;



    void Awake() {

        Instance = this;
    }

    void Start() {
    }

    void Update() {



        if (isBusy) {
            return;
        }

        if (!TurnSystem.Instance.IsPlayerTurn()) {
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape)) {
            SetSelectedAction(null);
        }


        if (EventSystem.current.IsPointerOverGameObject()) {
            return;
        }


        if (Input.GetMouseButtonDown(0)) {

            if (TryHandleUnitSelection()) {
                return;
            }

            if (selectedAction != null) {
                HandleSelectedAction();
            }

            selectedAction = null; // TODO: Only deselect if we clicked on a valid grid position for the action
        }


    }


    void HandleSelectedAction() {

        GridPosition gridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
        if (!selectedAction.IsValidActionGridPosition(gridPosition)) {
            Debug.LogError($"Invalid {selectedAction.GetActionName()} target at {gridPosition}.");
            return;
        }

        if (selectedUnit.TrySpendActionPoints(selectedAction)) {
            selectedAction.TakeAction(gridPosition);
            OnActionStarted?.Invoke(this, EventArgs.Empty);
        }


        // switch (selectedAction) {
        //     case MoveAction moveAction:
        //         GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
        //         if (!moveAction.IsValidActionGridPosition(mouseGridPosition))
        //             return;

        //         moveAction.Move(mouseGridPosition);
        //         break;
        //     case SpinAction spinAction:
        //         spinAction.Spin();
        //         break;
        //     default:
        //         throw new ArgumentOutOfRangeException();
        // }
    }

    bool TryHandleUnitSelection() {

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, unitLayerMask)) {
            if (hitInfo.transform.TryGetComponent<Unit>(out Unit unit)) {

                if (unit == selectedUnit) {
                    return false;
                }

                if (unit.IsEnemy()) {
                    return false;
                }

                SetSelectedUnit(unit);
                return true;
            }
        }

        return false;
    }



    void SetSelectedUnit(Unit unit) {
        selectedUnit = unit;

        SetSelectedAction(unit.GetMoveAction());

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);

    }


    public void SetSelectedAction(BaseAction action) {
        if (isBusy) {
            return;
        }

        selectedAction = action;
    }

    public BaseAction GetSelectedAction() {
        return selectedAction;
    }


    public Unit GetSelectedUnit() {
        return selectedUnit;
    }

    public bool IsBusy() {
        return isBusy;
    }

    public void SetBusy() {
        isBusy = true;
    }

    public void ClearBusy() {
        isBusy = false;
    }
}
