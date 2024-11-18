using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStats
{
    [Header("Enemy Variables")]
    public float EnemyDamage = 15f;
    public float EnemySpeed = 5f;
    public float EnemyAttackRange = 1f;
    public float EnemyDetectionRange = 10f;

    public GameObject Target;
    public GameObject EnemyWeapon;

    private float distancetoTarget;
    private bool attackingTarget;

    protected override void Start()
    {
        base.Start();

        attackingTarget = false;
    }

    protected void Update()
    {
        //calculates the distance between the enemy and the target
        distancetoTarget = Vector2.Distance(transform.position, Target.transform.position);
        Vector2 directiontoTarget = Target.transform.position - transform.position;
        directiontoTarget.Normalize();

        //calculates the enemy rotation
        float lookangle = Mathf.Atan2(directiontoTarget.y, directiontoTarget.x) * Mathf.Rad2Deg;

        //transform position and rotation is changed constantly to have the enemy walk towards the target and face the target.
        transform.position = Vector2.MoveTowards(transform.position, Target.transform.position, EnemySpeed * Time.deltaTime);
        transform.rotation = Quaternion.Euler(Vector3.forward * lookangle);
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
