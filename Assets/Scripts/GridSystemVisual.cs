using System.Collections.Generic;
using UnityEngine;

public class GridSystemVisual : MonoBehaviour {


    [SerializeField]
    private GameObject gridSystemVisualMeshPrefab;

    private GridSystemVisualMesh[,] gridSystemVisualMeshes;

    void Start() {

        gridSystemVisualMeshes = new GridSystemVisualMesh[LevelGrid.Instance.GetWidth(), LevelGrid.Instance.GetHeight()];

        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++) {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++) {
                GridPosition gridPosition = new GridPosition(x, z);
                Vector3 worldPosition = LevelGrid.Instance.GetWorldPosition(gridPosition);
                var gridSystemVisualMesh = GameObject.Instantiate(gridSystemVisualMeshPrefab, worldPosition, Quaternion.identity);
                gridSystemVisualMeshes[x, z] = gridSystemVisualMesh.GetComponent<GridSystemVisualMesh>();
            }
        }
    }

    void Update() {
        UpdateVisuals();
        
    }


    public void HideAllGridPosition() {
        for (int x = 0; x < LevelGrid.Instance.GetWidth(); x++) {
            for (int z = 0; z < LevelGrid.Instance.GetHeight(); z++) {
                gridSystemVisualMeshes[x, z].Hide();
            }
        }
    }

    public void ShowGridPositionList(List<GridPosition> gridPositions) {
        foreach (var gridPosition in gridPositions) {
            gridSystemVisualMeshes[gridPosition.X, gridPosition.Z].Show();
        }
    }

    void UpdateVisuals() {
        HideAllGridPosition();
        BaseAction selectedAction = UnitActionSystem.Instance.GetSelectedAction();
        if (selectedAction != null) {
            ShowGridPositionList(selectedAction.GetValidActionGridPositionList());
        }
    }
}

