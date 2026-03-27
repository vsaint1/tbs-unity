using UnityEngine;

public class MouseWorld : MonoBehaviour {


    [SerializeField]
    private LayerMask planeMask;

    private static MouseWorld instance;

    void Awake() {
        instance = this;
    }

    void Update() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, planeMask)) {
            transform.position = hitInfo.point;
        }
        
    }


    public static Vector3 GetPosition() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, float.MaxValue, instance.planeMask)) {
            return hitInfo.point;
        }

        
        return Vector3.zero;
    }

}