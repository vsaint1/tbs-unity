using UnityEngine;

public class GridDebugTest : MonoBehaviour {

    private GridSystem gridSystem;

    [SerializeField]
    private GameObject gridDebugObjectPrefab;

    void Start() {
        gridSystem = new GridSystem(10, 10);
        gridSystem.CreateDebugObject(gridDebugObjectPrefab);
    }

    void Update() {

    }
}
