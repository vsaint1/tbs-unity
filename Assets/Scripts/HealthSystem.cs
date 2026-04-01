using System;
using UnityEngine;

public class HealthSystem : MonoBehaviour {


    public event EventHandler OnHealthChanged;
    public event EventHandler OnDead;

    [SerializeField]
    private int health = 100;

    [SerializeField]
    private int maxHealth = 100;

    void Start() {

    }

    void Update() {

    }

    public void Damage(int amount) {
        health -= amount;

        if (health < 0) {
            health = 0;
        }

        OnHealthChanged?.Invoke(this, EventArgs.Empty);

        if (health <= 0) {
            OnDead?.Invoke(this, EventArgs.Empty);
            Destroy(gameObject);
        }

    }

    public int GetHealth() {
        return health;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public void SetHealth(int health) {
        this.health = health;
    }


    public float GetHealthNormalized() {
        return health / (float)maxHealth;
    }
    
    public void Kill() {
        health = 0;
        OnDead?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
