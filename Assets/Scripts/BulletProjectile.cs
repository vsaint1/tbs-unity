
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    private Vector3 targetPosition;

    private const float moveSpeed = 50f;


    public void SetTarget(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    void Update() {


        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveSpeed * Time.deltaTime * moveDirection;

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget < 0.1f) {
            Destroy(gameObject);
        }

    }

}