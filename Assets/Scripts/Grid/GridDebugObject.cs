using TMPro;
using UnityEngine;

public class GridDebugObject : MonoBehaviour {


    private GridObject gridObject;

    [SerializeField]
    private TextMeshPro textMeshPro;


    void Start() {

    }

    void Update() {
        if (gridObject != null) {
            textMeshPro.text = gridObject.ToString();
        }
    }

    public void SetGridObject(GridObject gridObject) {
        this.gridObject = gridObject;
        textMeshPro.text = this.gridObject.ToString();
    }
}
