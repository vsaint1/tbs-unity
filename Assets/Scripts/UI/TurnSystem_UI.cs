using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystemUI : MonoBehaviour {


    [SerializeField]
    private Button endTurnButton;

    [SerializeField]
    private TextMeshProUGUI turnTextTMP;

    [SerializeField]
    private TextMeshProUGUI turnIndicatorTMP;



    void Start() {
        turnTextTMP.text = "Turn: " + TurnSystem.Instance.GetCurrentTurn();

        TurnSystem.Instance.OnTurnChanged += TS_OnTurnChanged;
        endTurnButton.onClick.AddListener(CallNextTurn);
        UpdateTurnText();
        UpdateTurnIndicator();
    }

    void Update() {

    }

    void CallNextTurn() {
        TurnSystem.Instance.NextTurn();
    }

    void UpdateTurnText() {
        turnTextTMP.text = "Turn: " + TurnSystem.Instance.GetCurrentTurn();
    }

    void UpdateTurnIndicator() {
        turnIndicatorTMP.text = TurnSystem.Instance.IsPlayerTurn() ? "It's Your Turn!" : "Unit_Enemy is Playing!";
        turnIndicatorTMP.color = TurnSystem.Instance.IsPlayerTurn() ? Color.seaGreen : Color.red;
    }


    void TS_OnTurnChanged(object sender, System.EventArgs e) {
        UpdateTurnText();
        UpdateTurnIndicator();
    }


    void OnDestroy() {
        endTurnButton.onClick.RemoveAllListeners();
    }
}
