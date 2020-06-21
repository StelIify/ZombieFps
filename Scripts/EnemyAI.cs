using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    [SerializeField]float chaseDistance = 20f;
    [SerializeField]float turnSpeed = 5f;

    PlayerHealth target;
    NavMeshAgent navMeshAgent;
    Animator anim;
    EnemyHealth enemyHealth;
    bool isProvoked = false;
    float distanceBetweenTarget;
    private int damage = 50;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
        target = FindObjectOfType<PlayerHealth>();
        enemyHealth = GetComponent<EnemyHealth>();
    }

    private void OnEnable()
    {
        enemyHealth.OnDamageTaken += Provoke;
    }
    private void OnDisable()
    {
        enemyHealth.OnDamageTaken -= Provoke;
    }
    void Update()
    {
        
        if(target != null)
        CalculateChase();
    }

    private void CalculateChase()
    {
       distanceBetweenTarget = Vector3.Distance(transform.position, target.transform.position);
       

        if (isProvoked == true)
        {
            EngageTarget();
        }
        else if (chaseDistance >= distanceBetweenTarget)
        {
            isProvoked = true;
        }
           
    }

    private void EngageTarget()
    {
        FaceTarget();
        navMeshAgent.stoppingDistance = 2;
        if(navMeshAgent.stoppingDistance < distanceBetweenTarget)
        {
            ChaseTarget();
        }
        else if(navMeshAgent.stoppingDistance >= distanceBetweenTarget)
        {
            AttackTarget();
        }
    }
    private void ChaseTarget()
    {
       navMeshAgent.SetDestination(target.transform.position);
        anim.SetBool("Move", true);
        anim.SetBool("Attack", false);
    }

    private void AttackTarget()
    {
        if (target != null)
        { 
            anim.SetBool("Attack", true);
        }
      
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(165, 143, 221, 255f);
        Gizmos.DrawWireSphere(transform.position, chaseDistance);
    }

    public void Provoke()
    {
        isProvoked = true;
        
    }

    private void FaceTarget()
    {
        Vector3 direction = (target.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);
    }
}
