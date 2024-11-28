using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBullet : MonoBehaviour
{
    public SO_Weaponry SOWeapon;
    public LayerMask HittableLayer;

    private Rigidbody2D BulletRB;

    private Transform StartPos;

    private void Start()
    {
        BulletRB = GetComponent<Rigidbody2D>();

        StartPos = transform;

        float AngRad = Mathf.Atan2(BulletRB.velocity.x, BulletRB.velocity.y);
        float AngDeg = (180 / Mathf.PI) * AngRad - 90;

        transform.rotation = Quaternion.Euler(0, 0, AngDeg);
    }

    private void Update()
    {
        BulletRB.AddForce(transform.right * SOWeapon.BulletSpeed, ForceMode2D.Impulse);

        if (Vector2.Distance(StartPos.position,transform.position) > SOWeapon.MaxDistance)
        {
            Destroy(gameObject);
        }
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
                if (Hit = Physics2D.CircleCast(transform.position, SOWeapon.AOERange, transform.position))
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
}
