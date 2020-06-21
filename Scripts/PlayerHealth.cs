using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamagable
{
    [SerializeField]
    private int health = 200;

    PlayerDeathHandler deathHandler;
    private void Awake()
    {
        deathHandler = GetComponent<PlayerDeathHandler>();
    }
    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Debug.Log("Dead");
            deathHandler.HandleDeath();
        }
    }
}
