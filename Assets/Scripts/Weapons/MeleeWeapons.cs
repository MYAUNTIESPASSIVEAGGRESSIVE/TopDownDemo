using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapons : Weapons
{
    private bool TakingDamage;
    private float DamageTicker;

    private SO_Melee MeleeSO;

    public void MeleeUse(SO_Melee SOMelee)
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = true;


        MeleeSO = SOMelee;

        base.PlayShotAudio(SOMelee.MeleeAudio);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakingDamage = true;
            collision.transform.GetComponent<EnemyStats>().TakeDamage(MeleeSO.Damage, MeleeSO.Gore);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && TakingDamage)
        {

        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            TakingDamage = false;
        }
    }
}
