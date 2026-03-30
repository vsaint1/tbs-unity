using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour {

    public static TurnSystem Instance { get; private set; }

    public event EventHandler OnTurnChanged;
    
    private int turn = 1;

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

    public void NextTurn() {
        turn++;
        OnTurnChanged?.Invoke(this, EventArgs.Empty);
    }
}
