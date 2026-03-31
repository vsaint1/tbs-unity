using System.Collections.Generic;
using UnityEngine;

public class LevelGrid : MonoBehaviour {


    public static LevelGrid Instance { get; private set; }

    private GridSystem gridSystem;

    [SerializeField]
    private GameObject gridDebugObjectPrefab;

    void Awake() {
        Instance = this;
        gridSystem = new GridSystem(10, 10);

    }

    void Start() {
        gridSystem.CreateDebugObject(gridDebugObjectPrefab);
    }

    void Update() {

    }


    public void AddUnitAtGridPosition(GridPosition gridPosition, Unit unit) {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.AddUnit(unit);

    }

    public List<Unit> GetUnitAtGridPosition(GridPosition gridPosition) {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnits();

    }

    public void RemoveUnitAtGridPosition(GridPosition gridPosition, Unit unit) {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        gridObject.RemoveUnit(unit);
    }

    public GridPosition GetGridPosition(Vector3 worldPosition) {
        return gridSystem.GetGridPosition(worldPosition);
    }

    public Vector3 GetWorldPosition(GridPosition gridPosition) {
        return gridSystem.GetWorldPosition(gridPosition);
    }

    public bool IsValidGridPosition(GridPosition gridPosition) {
        return gridSystem.IsValidGridPosition(gridPosition);
    }

    public bool HasAnyUnitOnGridPosition(GridPosition gridPosition) {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.HasAnyUnit();
    }

    public void UnitMovedGridPosition(Unit unit, GridPosition from, GridPosition to) {
        RemoveUnitAtGridPosition(from, unit);
        AddUnitAtGridPosition(to, unit);
    }

    public Unit GetUnitAtGridPosition(GridPosition gridPosition, int index) {
        GridObject gridObject = gridSystem.GetGridObject(gridPosition);
        return gridObject.GetUnit(index);
    }
    
    public int GetWidth() {
        return gridSystem.Width;
    }

    public int GetHeight() {
        return gridSystem.Height;
    }

}
