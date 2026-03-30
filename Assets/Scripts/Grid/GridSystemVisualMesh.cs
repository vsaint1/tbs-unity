using UnityEngine;

public class GridSystemVisualMesh : MonoBehaviour {

    [SerializeField]
    private MeshRenderer meshRenderer;

    void Start() {

    }

    void Update() {

    }


    public void Show() {
        meshRenderer.enabled = true;
    }

    public void Hide() {
        meshRenderer.enabled = false;
    }
}
