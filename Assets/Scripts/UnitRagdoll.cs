using UnityEngine;

public class UnitRagdoll : MonoBehaviour {

    [SerializeField]
    private GameObject unitRagdollPrefab;

    private HealthSystem healthSystem;
    private bool isDead = false;

    [SerializeField]
    private Transform rootBoneTransform;

    void Start() {
        healthSystem = GetComponent<HealthSystem>();
        healthSystem.OnDead += HS_OnDead;
    }

    void HS_OnDead(object sender, System.EventArgs e) {
        if (isDead) return;
        isDead = true;

        GameObject ragdollObject = Instantiate(unitRagdollPrefab, transform.position, transform.rotation);

        Transform ragdollRootBone = FindChildByName(ragdollObject.transform, rootBoneTransform.name);

        if (ragdollRootBone != null) {
            MatchAllChildTransforms(rootBoneTransform, ragdollRootBone);
        }
        else {
            Debug.LogError($"Could not find bone '{rootBoneTransform.name}' in ragdoll prefab!");
        }

        ApplyExplosionForceToRagdoll(ragdollObject, transform.position, 100f, 1f);

    }

    void OnDestroy() {
        healthSystem.OnDead -= HS_OnDead;
    }

    Transform FindChildByName(Transform parent, string name) {
        foreach (Transform child in parent) {
            if (child.name == name) return child;
            Transform found = FindChildByName(child, name);
            if (found != null) return found;
        }

        return null;
    }

    void MatchAllChildTransforms(Transform source, Transform target) {
        foreach (Transform targetChild in target) {
            Transform sourceChild = source.Find(targetChild.name);
            if (sourceChild != null) {
                targetChild.SetPositionAndRotation(sourceChild.position, sourceChild.rotation);
                MatchAllChildTransforms(sourceChild, targetChild);
            }
        }
    }

    void ApplyExplosionForceToRagdoll(GameObject ragdoll, Vector3 explosionPosition, float explosionForce, float explosionRadius) {
        Rigidbody[] rigidbodies = ragdoll.GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigidbody in rigidbodies) {
            rigidbody.AddExplosionForce(explosionForce, explosionPosition, explosionRadius);
        }
    }
}