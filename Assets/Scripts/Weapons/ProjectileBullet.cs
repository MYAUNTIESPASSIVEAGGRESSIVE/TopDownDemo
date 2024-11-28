using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : Weapons
{
    public SO_Weaponry SOWeapon;
    public LayerMask HittableLayer;

    private Rigidbody2D BulletRB;

    private void Start()
    {
        BulletRB = GetComponent<Rigidbody2D>();

        Vector3 MPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 movedirection = MPos - transform.position;
        Vector3 rot = transform.position - MPos;

        BulletRB.velocity = new Vector2(movedirection.x, movedirection.y).normalized * SOWeapon.BulletSpeed;
        float rotation = Mathf.Atan2(rot.y, rot.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rotation + 90);

        StartCoroutine(DestroyTimer());

    }

    public override void PlayShotAudio(AudioClip[] ShootSound)
    {

        ShootSound = SOWeapon.WeaponAudio;

        base.PlayShotAudio(ShootSound);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(SOWeapon.ExplodeOnImpact)
        {
            RaycastHit2D Hit;
            if(Hit = Physics2D.CircleCast(transform.position, SOWeapon.AOERange, transform.position, 5, HittableLayer))
            {
                if (Hit.transform.CompareTag("Enemy"))
                {
                    Hit.transform.GetComponent<EnemyStats>().TakeDamage(SOWeapon.Damage, true);
                    Destroy(gameObject);
                }
                else
                {
                    Destroy(gameObject);
                }
            }
        }

        if(collision.transform.CompareTag("Enemy"))
        {
            if (SOWeapon.ExplodeOnImpact)
            {
                collision.transform.GetComponent<EnemyStats>().TakeDamage(SOWeapon.Damage, true);

                RaycastHit2D Hit;

                if (Hit = Physics2D.CircleCast(transform.position, SOWeapon.AOERange, transform.position, 5, HittableLayer))
                {
                    Hit.transform.GetComponent<EnemyStats>().TakeDamage(SOWeapon.Damage, true);
                }

                Destroy(gameObject);
            }
            else
            {
                collision.transform.GetComponent<EnemyStats>().TakeDamage(SOWeapon.Damage, false);
                Destroy(gameObject);
            }
        }
    }

    private IEnumerator DestroyTimer()
    {
        yield return new WaitForSecondsRealtime(3);
        Destroy(gameObject);
    }
}
