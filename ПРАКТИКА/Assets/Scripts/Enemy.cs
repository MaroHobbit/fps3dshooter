using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float angle = 30f; // ”гол поворота в градусах
    [SerializeField] private int HP = 100;
    private Animator animator;
    private NavMeshAgent navAgent;
    public bool isDead = false;
    

    private void Start()
    {
        
        animator = GetComponent<Animator>();
        navAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        
    }

    public void TakeDamage(int damageAmount)
    {
        HP -= damageAmount;
        print($"Enemy hp = {HP}");

        if (HP < 1)
        {
            navAgent.enabled = false;

            int randomValue = UnityEngine.Random.Range(0, 3);

            if (randomValue >= -1)
            {
                animator.SetTrigger("Die1");
            }

            isDead = true;

            SoundManager.Instance.zombieChannel.Stop();
            SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.zombieDeath);
        }
        else
        {
            SoundManager.Instance.zombieChannel.Stop();
            SoundManager.Instance.zombieChannel2.PlayOneShot(SoundManager.Instance.zombieHurt);
            animator.SetTrigger("Damage");
        }

    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 4f); // Attaking / StopAttaking

        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 10f); // DetectionArea (StartChasing)

        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, 21f); // StopChasing
    }

    
}
