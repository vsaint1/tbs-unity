using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class Unit : MonoBehaviour {


    private Vector3 targetPosition;

    void Start() {

    }

    void Update() {


        if (Input.GetMouseButtonDown(0)) {
            Move(MouseWorld.GetPosition());
        }

        float moveSpeed = 5f;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

    }


    private void Move(Vector3 position) {
        targetPosition = position;
    }
}
