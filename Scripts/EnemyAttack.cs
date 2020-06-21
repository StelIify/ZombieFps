using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    [SerializeField] int damage = 20;
    PlayerHealth player;
    private void Awake()
    {
        player = FindObjectOfType<PlayerHealth>();
    }


    public void AttackHitEvent()
    {
        if(player != null)
        {
            player.TakeDamage(damage);
            Debug.Log("Bang Bang");
        }
    }

    
}
