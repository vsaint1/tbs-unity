using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystemUI : MonoBehaviour {


    [SerializeField]
    private GameObject actionButtonPrefab;

    [SerializeField]
    private Transform actionButtonsContainer;

    [SerializeField]
    private TextMeshProUGUI actionPointsTMP;

    void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UAS_OnSelectedUnitChanged;
        UnitActionSystem.Instance.OnActionStarted += UAS_OnActionStarted;
        TurnSystem.Instance.OnTurnChanged += TS_OnTurnChanged; /// NOTE: this can have order issues
    }

    void Update() {

    }

    private void CreateUnitActionButtons() {
        foreach (Transform buttonTransform in actionButtonsContainer) {
            Destroy(buttonTransform.gameObject);
        }

        Unit unit = UnitActionSystem.Instance.GetSelectedUnit();
        actionPointsTMP.text = $"Action Points: {unit.GetActionPoints()}";

        foreach (BaseAction action in unit.GetActions()) {
            GameObject actionButtonGameObject = Instantiate(actionButtonPrefab);
            actionButtonGameObject.transform.SetParent(actionButtonsContainer, false);

            ActionButtonUI actionButtonUI = actionButtonGameObject.GetComponent<ActionButtonUI>();
            actionButtonUI.SetAction(action);
        }

    }


    void UpdatePointsText() {
        actionPointsTMP.text = $"Action Points: {UnitActionSystem.Instance.GetSelectedUnit().GetActionPoints()}";
    }

    void UAS_OnSelectedUnitChanged(object sender, EventArgs e) {
        CreateUnitActionButtons();
    }

    void UAS_OnActionStarted(object sender, EventArgs e) {
        UpdatePointsText();
    }

    void TS_OnTurnChanged(object sender, EventArgs e) {
        UpdatePointsText();
    }
}
