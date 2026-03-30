using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {



    [SerializeField]
    private TextMeshProUGUI textMeshPro;

    [SerializeField]
    private Button button;


    private Outline outline;


    void Awake() {
        outline = GetComponent<Outline>();
    }


    void Start() {

    }

    void Update() {
    }

    public void SetAction(BaseAction action) {
        textMeshPro.text = action.GetActionName().ToUpper();

        button.onClick.AddListener(() => {
            UnitActionSystem.Instance.SetSelectedAction(action);
        });
    }


    private void OnDestroy() {
        button.onClick.RemoveAllListeners();
    }

    public void OnPointerEnter(PointerEventData eventData) {
        outline.effectColor = Color.white;
        outline.effectDistance = new Vector2(3, 3);
    }

    public void OnPointerExit(PointerEventData eventData) {
        outline.effectColor = Color.black;
        outline.effectDistance = new Vector2(2, 2);
    }

}
