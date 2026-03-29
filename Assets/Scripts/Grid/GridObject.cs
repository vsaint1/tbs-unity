
using UnityEngine;

public class GridObject {

    public GridSystem GridSystem { get; private set; }
    public GridPosition GridPosition { get; private set; }

    public GridObject(GridSystem gridSystem, GridPosition gridPosition) {
        GridSystem = gridSystem;
        GridPosition = gridPosition;
    }




}