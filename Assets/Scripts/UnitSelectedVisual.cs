using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class UnitSelectedVisual : MonoBehaviour {


    private MeshRenderer meshRenderer;

    [SerializeField]
    private Unit unit;


    void Awake() {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UAS_OnSelectedUnitChanged;
        UpdateVisual();
    }

    void Update() {

    }


    void UAS_OnSelectedUnitChanged(object sender, System.EventArgs e) {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit) {
            if (meshRenderer != null)
                meshRenderer.enabled = true;
        }
        else {
            if (meshRenderer != null)

                meshRenderer.enabled = false;
        }
    }


    void UpdateVisual() {
        if (UnitActionSystem.Instance.GetSelectedUnit() == unit) {
            if (meshRenderer != null)
                meshRenderer.enabled = true;
        }
        else {
            if (meshRenderer != null)
                meshRenderer.enabled = false;
        }
    }
}
