using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyStats : EntityStats
{
    [Header("Enemy Variables")]
    public float EnemyDamage = 15f;
    public float EnemySpeed = 5f;
    public float EnemyAttackRange = 1f;
    public float EnemyDetectionRange = 10f;

    public GameObject Target;
    //public GameObject EnemyWeapon;

    private float distancetoTarget;
    //private bool attackingTarget;

    protected override void Start()
    {
        base.Start();

        //attackingTarget = false;
    }

    protected void Update()
    {
        EnemyMove();
        EnemyLook();
    }

    private void EnemyMove()
    {

    }

    private void EnemyLook()
    {

    }

    public override void TakeDmg(float Dmg)
    {
        if (IsDead) return;

        base.TakeDmg(Dmg);
    }

    protected override void DeathLogic()
    {
        if (IsDead) return;

        CurrentHealth = 0;

        IsDead = true;
    }

    private void EnemyAttackTarget()
    {

    }

}
