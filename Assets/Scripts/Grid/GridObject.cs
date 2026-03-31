
using System;
using System.Collections.Generic;
using UnityEngine;

public class GridObject {

    public GridSystem GridSystem { get; private set; }
    public GridPosition GridPosition { get; private set; }
    private List<Unit> units;

    public GridObject(GridSystem gridSystem, GridPosition gridPosition) {
        GridSystem = gridSystem;
        GridPosition = gridPosition;
        units = new List<Unit>();
    }

    public void AddUnit(Unit unit) {
        units.Add(unit);
    }

    public void RemoveUnit(Unit unit) {
        units.Remove(unit);
    }

    public List<Unit> GetUnits() {
        return units;
    }

    // public void ClearUnit() {
    //     units.Clear();
    // }

    public Unit GetUnit(int index) {
        if (HasAnyUnit()) {
            return units[index];
        }
        else {
            return null;
        }
    }

    public bool HasAnyUnit() {
        return units.Count > 0;
    }

    public override string ToString() {

        String str = "";
        foreach (Unit unit in units) {
            str += unit + "\n";

        }
        return $"{GridPosition}\n {str}";

    }

}