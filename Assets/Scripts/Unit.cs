using UnityEngine;

public class Unit : MonoBehaviour {


    private Vector3 targetPosition;

    private readonly float moveSpeed = 5f;

    [SerializeField]
    private Animator animator;

    void Awake() {
        targetPosition = transform.position;
    }

    void Start() {

    }

    void Update() {


        if (Vector3.Distance(transform.position, targetPosition) > 0.1f) {

            Vector3 direction = (targetPosition - transform.position).normalized;
            if (direction != Vector3.zero) {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                const float rotateSpeed = 10f;
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * rotateSpeed);
            }

            transform.position += moveSpeed * Time.deltaTime * direction;

            animator.SetBool("IsMoving", true);


        }
        else {
            animator.SetBool("IsMoving", false);

        }

    }


    public void Move(Vector3 position) {
        targetPosition = position;
    }
}
