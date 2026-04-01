using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UnitWorldUI : MonoBehaviour {



    [SerializeField]
    private Image healthBarFill;

    [SerializeField]
    private TextMeshProUGUI actionsPointTMP;

    private HealthSystem healthSystem;

    private Unit unit;

    void Start() {

        healthSystem = GetComponentInParent<HealthSystem>();


        unit = GetComponentInParent<Unit>();

        actionsPointTMP.text = unit.GetActionPoints().ToString();

        healthBarFill.fillAmount = healthSystem.GetHealthNormalized();
        healthSystem.OnHealthChanged += HS_OnHealthChanged;
        unit.OnAnyActionPointsChanged += UP_OnActionPointsChanged;

        UpdateText();
        UpdateVisual();

    }

    void UpdateText() {
        actionsPointTMP.text = unit.GetActionPoints().ToString();
    }

    void UpdateVisual() {
        healthBarFill.fillAmount = healthSystem.GetHealthNormalized();
    }

    void UP_OnActionPointsChanged(object sender, System.EventArgs e) {
        UpdateText();
    }

    void HS_OnHealthChanged(object sender, System.EventArgs e) {
        UpdateVisual();

    }

    void OnDestroy() {
        healthSystem.OnHealthChanged -= HS_OnHealthChanged;
        unit.OnAnyActionPointsChanged -= UP_OnActionPointsChanged;
    }



}