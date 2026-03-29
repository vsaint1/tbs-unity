using System;
using UnityEngine;

public class UnitActionSystem : MonoBehaviour {


    public static UnitActionSystem Instance { get; private set; }
    public event EventHandler OnSelectedUnitChanged;


    private Unit selectedUnit;

    [SerializeField]
    private LayerMask unitLayerMask;

    private GridSystem gridSystem;

    [SerializeField]
    private GameObject gridDebugObjectPrefab;

    void Awake() {

        Instance = this;
    }

    void Start() {
        gridSystem = new GridSystem(10, 10);
        gridSystem.CreateDebugObject(gridDebugObjectPrefab);
    }

    void Update() {
        if (!Input.GetMouseButtonDown(0)) {
            return;
        }

        if (TryHandleUnitSelection()) {
            return;
        }

        if (selectedUnit != null) {
            selectedUnit.Move(MouseWorld.GetPosition());

        }

        Debug.Log(gridSystem.GetGridPosition(MouseWorld.GetPosition()));
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
