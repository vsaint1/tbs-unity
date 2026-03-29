using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour {


    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;


    private Unit selectedUnit;

    [SerializeField]
    private LayerMask unitLayerMask;

 

    void Awake() {

        Instance = this;
    }

    void Start() {
    }

    void Update() {
        if (!Input.GetMouseButtonDown(0)) {
            return;
        }

        if (TryHandleUnitSelection()) {
            return;
        }

        if (selectedUnit != null) {
            GridPosition mouseGridPosition = LevelGrid.Instance.GetGridPosition(MouseWorld.GetPosition());
            if (!LevelGrid.Instance.IsValidGridPosition(mouseGridPosition)) {
                return;
            }

            Vector3 targetWorldPosition = LevelGrid.Instance.GetWorldPosition(mouseGridPosition);
            selectedUnit.Move(targetWorldPosition);

        }

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

        OnSelectedUnitChanged?.Invoke(this, EventArgs.Empty);

    }


    public Unit GetSelectedUnit() {
        return selectedUnit;
    }
}
