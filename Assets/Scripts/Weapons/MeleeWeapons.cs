using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeWeapons : Weapons
{

    public void MeleUse(SO_Melee SOMelee, bool ShootOut)
    {
        if (!ShootOut)
        {

        }
        else
        {

        }

        base.PlayShotAudio(SOMelee.MeleeAudio,SOMelee.MeleeSource);
    }
}
