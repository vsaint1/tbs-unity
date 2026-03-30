using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UnitActionSystemUI : MonoBehaviour {


    [SerializeField]
    private GameObject actionButtonPrefab;

    [SerializeField]
    private Transform actionButtonsContainer;

    void Start() {
        UnitActionSystem.Instance.OnSelectedUnitChanged += UAS_OnSelectedUnitChanged;
    }

    void Update() {

    }

    private void CreateUnitActionButtons() {
        foreach (Transform buttonTransform in actionButtonsContainer) {
            Destroy(buttonTransform.gameObject);
        }

        Unit unit = UnitActionSystem.Instance.GetSelectedUnit();

        foreach (BaseAction action in unit.GetActions()) {
            GameObject actionButtonGameObject = Instantiate(actionButtonPrefab);
            actionButtonGameObject.transform.SetParent(actionButtonsContainer, false);

            ActionButtonUI actionButtonUI = actionButtonGameObject.GetComponent<ActionButtonUI>();
            actionButtonUI.SetAction(action);
        }

    }


    void UAS_OnSelectedUnitChanged(object sender, EventArgs e) {
        CreateUnitActionButtons();
    }
}
