using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float rotationSpeed = 100f;
    [SerializeField] private float zoomSpeed = 2f;
    private const float MIN_DISTANCE = 1f;
    private const float MAX_DISTANCE = 50f;

    [SerializeField] private CinemachineCamera cinemachineCamera;
    private CinemachinePositionComposer positionComposer;

    void Start() {

        positionComposer = cinemachineCamera.GetCinemachineComponent(CinemachineCore.Stage.Body) as CinemachinePositionComposer;
    }

    void LateUpdate() {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) dir.z += 1f;
        if (Input.GetKey(KeyCode.S)) dir.z -= 1f;
        if (Input.GetKey(KeyCode.A)) dir.x -= 1f;
        if (Input.GetKey(KeyCode.D)) dir.x += 1f;

        Vector3 moveVector = (transform.forward * dir.z + transform.right * dir.x).normalized;
        transform.position += moveSpeed * Time.deltaTime * moveVector;

        float rotY = 0f;
        if (Input.GetKey(KeyCode.Q)) rotY += 1f;
        if (Input.GetKey(KeyCode.E)) rotY -= 1f;
        transform.eulerAngles += new Vector3(0f, rotationSpeed * Time.deltaTime * rotY, 0f);

        float scroll = Input.mouseScrollDelta.y;
        if (positionComposer != null && scroll != 0f) {
            positionComposer.CameraDistance -= scroll * zoomSpeed;
            positionComposer.CameraDistance = Mathf.Clamp(positionComposer.CameraDistance, MIN_DISTANCE, MAX_DISTANCE);
        }
    }
}