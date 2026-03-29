

using System;
using UnityEngine;


public struct GridPosition : IEquatable<GridPosition> {
    public int X;
    public int Z;

    public GridPosition(int x, int z) {
        X = x;
        Z = z;
    }

    public override readonly string ToString() {
        return $"({X}, {Z})";
    }

    public bool Equals(GridPosition other) {
        return X == other.X && Z == other.Z;
    }

    public override bool Equals(object obj) {
        return obj is GridPosition position && Equals(position);
    }

    public override int GetHashCode() {
        return HashCode.Combine(X, Z);
    }

    public static bool operator ==(GridPosition left, GridPosition right) {
        return left.Equals(right);
    }

    public static bool operator !=(GridPosition left, GridPosition right) {
        return !(left == right);
    }
}


public class GridSystem {


    private int width;
    private int height;
    private const float cellSize = 2f;

    private GridObject[,] gridObjectArray;

    public GridSystem(int width, int height) {
        this.width = width;
        this.height = height;

        gridObjectArray = new GridObject[width, height];

        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                // Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z) + Vector3.right * .2f, Color.white, 1000f);
                var gridPosition = new GridPosition(x, z);
                gridObjectArray[x, z] = new GridObject(this, gridPosition);
            }
        }


    }


    public Vector3 GetWorldPosition(GridPosition gridPosition) {
        return new Vector3(gridPosition.X, 0, gridPosition.Z) * cellSize;
    }


    public GridPosition GetGridPosition(Vector3 worldPosition) {

        return new GridPosition(
            Mathf.RoundToInt(worldPosition.x / cellSize),
            Mathf.RoundToInt(worldPosition.z / cellSize)
        );
    }

    public void CreateDebugObject(GameObject prefab) {
        for (int x = 0; x < width; x++) {
            for (int z = 0; z < height; z++) {
                var gridPosition = new GridPosition(x, z);
                var worldPosition = GetWorldPosition(gridPosition);
                var debugObject = GameObject.Instantiate(prefab, worldPosition, Quaternion.identity);

                var gridDebugObject = debugObject.GetComponent<GridDebugObject>();

                gridDebugObject.SetGridObject(GetGridObject(gridPosition));
            }
        }
    }

    public GridObject GetGridObject(GridPosition gridPosition) {
        return gridObjectArray[gridPosition.X, gridPosition.Z];
    }

    public bool IsValidGridPosition(GridPosition gridPosition) {
        return gridPosition.X >= 0 &&
               gridPosition.Z >= 0 &&
               gridPosition.X < width &&
               gridPosition.Z < height;
    }
}