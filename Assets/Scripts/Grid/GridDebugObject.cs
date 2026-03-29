using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour {


    private GridObject gridObject;

    [SerializeField]
    private TextMeshPro textMeshPro;


    void Start() {

    }

    void Update() {

    }

    public void SetGridObject(GridObject gridObject) {
        this.gridObject = gridObject;
        textMeshPro.text = this.gridObject.GridPosition.ToString();
    }
}
