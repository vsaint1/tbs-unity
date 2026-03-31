using UnityEngine;

public class EnemySystemAI : MonoBehaviour {


    void Start() {
        TurnSystem.Instance.OnTurnChanged += TS_OnTurnChanged;
    }

    void Update() {
        if (TurnSystem.Instance.IsPlayerTurn()) {
            return;
        }

    }


    void TS_OnTurnChanged(object sender, System.EventArgs e) {
        if (!TurnSystem.Instance.IsPlayerTurn()) {
            Debug.Log("Enemy's turn! AI would do something here...");
        }
    }
}
