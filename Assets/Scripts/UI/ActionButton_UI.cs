using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionButtonUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler {
    private static readonly Color ColorSelected = Color.seaGreen;
    private static readonly Color ColorHovered = new Color(1f, 1f, 1f);          // white
    private static readonly Color ColorDefault = new Color(0.15f, 0.15f, 0.15f); // near black

    private static readonly Vector2 OutlineSelected = new Vector2(4, 4);
    private static readonly Vector2 OutlineHovered = new Vector2(3, 3);
    private static readonly Vector2 OutlineDefault = new Vector2(1,1);

    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private Button button;

    private Outline outline;
    private bool isSelected;

    void Awake() {
        outline = GetComponent<Outline>();
        Navigation nav = button.navigation;
        nav.mode = Navigation.Mode.None;
        button.navigation = nav;
    }

    void Start() => ApplyOutline(ColorDefault, OutlineDefault);

    public void SetAction(BaseAction action) {
        textMeshPro.text = action.GetActionName().ToUpper();
        button.onClick.AddListener(() => {
            UnitActionSystem.Instance.SetSelectedAction(action);
            EventSystem.current.SetSelectedGameObject(gameObject);
        });
    }

    public void OnSelect(BaseEventData eventData) {
        isSelected = true;
        ApplyOutline(ColorSelected, OutlineSelected);
    }

    public void OnDeselect(BaseEventData eventData) {
        isSelected = false;
        ApplyOutline(ColorDefault, OutlineDefault);
    }

    public void OnPointerEnter(PointerEventData eventData)
        => ApplyOutline(ColorHovered, OutlineHovered);

    public void OnPointerExit(PointerEventData eventData)
        => ApplyOutline(isSelected ? ColorSelected : ColorDefault,
                        isSelected ? OutlineSelected : OutlineDefault);

    private void ApplyOutline(Color color, Vector2 distance) {
        if (!outline) return;
        outline.effectColor = color;
        outline.effectDistance = distance;
    }

    private void OnDestroy() => button.onClick.RemoveAllListeners();
}