using UnityEngine;
public class LookAtCamera : MonoBehaviour {

    private Transform mainCameraTransform;

    void Start() {
        mainCameraTransform = Camera.main.transform;
    }

    void LateUpdate() {
        Vector3 lookAtPosition = transform.position + mainCameraTransform.forward;
        transform.LookAt(lookAtPosition);
    }
}