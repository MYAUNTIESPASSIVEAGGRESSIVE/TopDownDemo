using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class EnemyStats : EntityStats
{
    [Header("Enemy Variables")]
    public Rigidbody2D EnemyRB;
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

        EnemyRB = GetComponent<Rigidbody2D>();

        //attackingTarget = false;
    }

    protected void Update()
    {
        EnemyLook();
        EnemyMove();
    }

    private void EnemyLook()
    {
        Vector3 lookpoint = Target.transform.position;

        Vector3 direction = lookpoint - transform.position;

        float ObjRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        EnemyRB.rotation = ObjRot;
    }

    private void EnemyMove()
    {
        EnemyRB.AddForce(transform.up * EnemySpeed, ForceMode2D.Force);
    }

    public override void TakeDamage(float Dmg)
    {
        if (IsDead) return;

        base.TakeDamage(Dmg);
    }

    protected override void EntityDeath()
    {
        if (IsDead) return;

        CurrentHealth = 0;

        IsDead = true;
    }

    private void EnemyAttackTarget()
    {

    }

}
