using UnityEngine;

public class Unit : MonoBehaviour {


    private const int ACTION_POINTS_MAX = 2;


    private GridPosition gridPosition;


    [SerializeField]
    private bool isEnemy;

    [SerializeField]
    private int actionPoints = ACTION_POINTS_MAX;

    public HealthSystem HealthSystem { get; private set; }
    private MoveAction moveAction;
    private SpinAction spinAction;

    private BaseAction[] actions;


    void Start() {
        moveAction = GetComponent<MoveAction>();
        spinAction = GetComponent<SpinAction>();
        actions = GetComponents<BaseAction>();
        HealthSystem = GetComponent<HealthSystem>();

        gridPosition = LevelGrid.Instance.GetGridPosition(transform.position);

        LevelGrid.Instance.AddUnitAtGridPosition(gridPosition, this);

        TurnSystem.Instance.OnTurnChanged += TS_OnTurnChanged;

        HealthSystem.OnDead += HS_OnDead;
    }

    void Update() {


        GridPosition newGridPosition = LevelGrid.Instance.GetGridPosition(transform.position);
        if (!newGridPosition.Equals(gridPosition)) {
            LevelGrid.Instance.UnitMovedGridPosition(this, gridPosition, newGridPosition);
            gridPosition = newGridPosition;
        }

    }


    public MoveAction GetMoveAction() {
        return moveAction;
    }

    public SpinAction GetSpinAction() {
        return spinAction;
    }

    public BaseAction[] GetActions() {
        return actions;
    }

    public GridPosition GetGridPosition() {
        return gridPosition;
    }

    public void TakeDamage(int damageAmount) {
        HealthSystem.Damage(damageAmount);
        Debug.Log($"Unit {name} took {damageAmount} damage, health is now {HealthSystem.GetHealth()}");
    }

    public bool IsEnemy() {
        return isEnemy;
    }

    public int GetActionPoints() {
        return actionPoints;
    }

    public bool TrySpendActionPoints(BaseAction action) {
        if (actionPoints >= action.GetActionPointCost()) {
            actionPoints -= action.GetActionPointCost();
            return true;
        }

        Debug.Log($"Not enough action points! Current: {actionPoints}, required: {action.GetActionPointCost()}");
        return false;
    }

    void TS_OnTurnChanged(object sender, System.EventArgs e) {
        if (IsEnemy() && !TurnSystem.Instance.IsPlayerTurn()
            || (!IsEnemy() && TurnSystem.Instance.IsPlayerTurn())) {

            actionPoints = ACTION_POINTS_MAX;

        }

    }

    void HS_OnDead(object sender, System.EventArgs e) {
        LevelGrid.Instance.RemoveUnitAtGridPosition(gridPosition, this);
        Destroy(gameObject);
    }

     void OnDestroy() {
        TurnSystem.Instance.OnTurnChanged -= TS_OnTurnChanged;
        HealthSystem.OnDead -= HS_OnDead;
    }
}
