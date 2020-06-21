using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour, IDamagable
{
    public delegate void DamageTaken();
    public event DamageTaken OnDamageTaken;

    [SerializeField] int maxHealth = 200;

    [SerializeField]
    int currentHealth;
    private void Awake()
    {
       // enemyAI = GetComponent<EnemyAI>();
        currentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void TakeDamage(int amount)
    {
        OnDamageTaken?.Invoke();
        currentHealth -= amount;
        if(currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }

    
}
