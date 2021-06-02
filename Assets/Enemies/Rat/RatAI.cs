using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.AI;

public class RatAI : Enemy
{
    protected override void FixedUpdate()
    {
        ChasePlayer();
    }
    protected override void Attack()
    {
        if (Time.time > lastAttacked + attackSpeed)
        {
            animator.SetTrigger("isAttacking");
            playerHealth.takeDamage(damage);
            lastAttacked = Time.time;
        }
    }
}
