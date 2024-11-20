using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : EntityStats
{
    protected override void Start()
    {
        base.Start();
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
        transform.GetComponent<PlayerControl>().enabled = false;
        transform.GetComponent<PlayerWeaponSelect>().enabled = false;
        IsDead = true;

    }
}
