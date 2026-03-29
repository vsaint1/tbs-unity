

using UnityEngine;


public struct GridPosition {
    public int x;
    public int z;

    public GridPosition(int x, int z) {
        this.x = x;
        this.z = z;
    }

    public override readonly string ToString() {
        return $"({x}, {z})";
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
        return new Vector3(gridPosition.x, 0, gridPosition.z) * cellSize;
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
        return gridObjectArray[gridPosition.x, gridPosition.z];
    }
}