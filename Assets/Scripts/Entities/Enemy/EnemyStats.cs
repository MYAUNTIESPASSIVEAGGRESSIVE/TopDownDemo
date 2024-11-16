using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : EntityStats
{
    protected override void Start()
    {
        base.Start();
    }

    protected override void DeathLogic()
    {
        if (IsDead) return;

        CurrentHealth = 0;


        IsDead = true;

    }
}
