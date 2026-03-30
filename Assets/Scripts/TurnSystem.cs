using UnityEngine;

public class TurnSystem : MonoBehaviour {



    private int number = 1;

    void Start() {

    }

    void Update() {

    }


    public void NextTurn() {
        number++;
        Debug.Log("Turn: " + number);
    }
}
