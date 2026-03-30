using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour {


    [SerializeField]
    private Button endTurnButton;

    [SerializeField]
    private TextMeshProUGUI turnTextTMP;



    void Start() {
        turnTextTMP.text = "Turn: " + TurnSystem.Instance.GetCurrentTurn();

        TurnSystem.Instance.OnTurnChanged += TS_OnTurnChanged;
        endTurnButton.onClick.AddListener(CallNextTurn);
        UpdateTurnText();
    }

    void Update() {

    }

    void CallNextTurn() {
        TurnSystem.Instance.NextTurn();
    }

    void UpdateTurnText() {
        turnTextTMP.text = "Turn: " + TurnSystem.Instance.GetCurrentTurn();
    }


    void TS_OnTurnChanged(object sender, System.EventArgs e) {
        UpdateTurnText();
    }


    void OnDestroy() {
        endTurnButton.onClick.RemoveAllListeners();
    }
}
