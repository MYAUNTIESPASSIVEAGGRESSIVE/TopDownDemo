using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : EntityStats
{
    public Slider HealthSlider;
    public int Lives = 3;
    public Image PlayerFaceBase;

    public Sprite PlayerState1;
    public Sprite PlayerState2;
    public Sprite PlayerState3;
    public Sprite PlayerState4;

    public Canvas DeathScreen;

    protected override void Start()
    {
        base.Start();

        HealthSlider.maxValue = MaxHealth;

        PlayerFaceBase.sprite = PlayerState1;
    }

    protected override void Update()
    {
        base.Update();

        HealthSlider.value = CurrentHealth;

        if(CurrentHealth > 75)
        {
            PlayerFaceBase.sprite = PlayerState1;
        }
        
        if (CurrentHealth <=  75)
        {
            PlayerFaceBase.sprite = PlayerState2;
        }
        
        if (CurrentHealth <= 50)
        {
            PlayerFaceBase.sprite = PlayerState3;
        }
        
        if (CurrentHealth <= 25)
        {
            PlayerFaceBase.sprite = PlayerState4;
        }

        if (Lives == 0)
        {
            CurrentHealth = 0;
            transform.GetComponent<PlayerControl>().enabled = false;
            transform.GetComponent<PlayerControl>().PlayerDead = true;
            transform.GetComponent<PlayerWeaponSelect>().enabled = false;
            IsDead = true;

            DeathScreen.gameObject.SetActive(true);
        }
    }

    public override void TakeDamage(float Dmg, bool Gore)
    {
        if (IsDead) return;

        base.TakeDamage(Dmg, Gore);


    }

    protected override void EntityDeath(bool Gore)
    {
        if (IsDead) return;
        Lives = Lives - 1;
        GameManager.Instance.Respawn();
        GameManager.Instance.PlayerPoints -= 500;

    }
}
