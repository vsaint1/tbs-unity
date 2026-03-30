using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class UnitActionSystem : MonoBehaviour {


    public static UnitActionSystem Instance { get; private set; }

    public event EventHandler OnSelectedUnitChanged;

    private Unit selectedUnit;

    [SerializeField]
    private LayerMask unitLayerMask;

    private BaseAction selectedAction;



    void Awake() {

        Instance = this;
    }

    void Start() {
    }

    void Update() {

        if(EventSystem.current.IsPointerOverGameObject()) {
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
        selectedAction.TakeAction(gridPosition);
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
        selectedAction = action;
    }


    public Unit GetSelectedUnit() {
        return selectedUnit;
    }
}
