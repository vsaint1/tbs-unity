using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour {

    public static TurnSystem Instance { get; private set; }

    public event EventHandler OnTurnChanged;

    private int turn = 1;

    private bool isPlayerTurn = true;

    void Awake() {
        Instance = this;
    }

    void Start() {

    }

    void Update() {

    }

    public int GetCurrentTurn() {
        return turn;
    }

    public bool IsPlayerTurn() {
        return isPlayerTurn;
    }

    public void NextTurn() {
        turn++;
        isPlayerTurn = !isPlayerTurn;
        Debug.Log("Turn changed! It's now " + (isPlayerTurn ? "Player's" : "Enemy's") + " turn.");
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }
}
