
using UnityEngine;

public class BulletProjectile : MonoBehaviour {

    private Vector3 targetPosition;
    private bool hasHit;

    [SerializeField]
    private GameObject bulletProjectileVFX;

    private const float moveSpeed = 50f;

    private void Awake() {
        if (bulletProjectileVFX != null) {
            bulletProjectileVFX.SetActive(false);
        }
    }


    public void SetTarget(Vector3 targetPosition) {
        this.targetPosition = targetPosition;
    }

    void Update() {

        if (hasHit) return;


        Vector3 moveDirection = (targetPosition - transform.position).normalized;
        transform.position += moveSpeed * Time.deltaTime * moveDirection;

        float distanceToTarget = Vector3.Distance(transform.position, targetPosition);
        if (distanceToTarget < 0.1f) {
            hasHit = true;

            if (bulletProjectileVFX != null) {
                bulletProjectileVFX.transform.SetParent(null, true);
                bulletProjectileVFX.SetActive(true);
                Destroy(bulletProjectileVFX, 2f);

            }

            Destroy(gameObject);
        }

    }

}