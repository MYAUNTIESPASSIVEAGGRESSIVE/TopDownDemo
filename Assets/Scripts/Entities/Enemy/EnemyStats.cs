using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStats
{
    public GameObject Target;
    public float EnemyDamage = 15f;
    public float EnemySpeed = 5f;
    public float EnemyAttackRange = 1f;

    protected override void Start()
    {
        base.Start();
    }

    protected void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, EnemySpeed * Time.deltaTime);

        if(Vector2.Distance(transform.position, Target.transform.position) > EnemyAttackRange)
        {
            EnemyAttackTarget();
        }
    }

    protected override void DeathLogic()
    {
        if (IsDead) return;

        CurrentHealth = 0;

        IsDead = true;
    }

    private void EnemyAttackTarget()
    {
        //do enemy attacking stuff.
    }
}
