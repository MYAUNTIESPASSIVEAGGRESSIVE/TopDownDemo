using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public bool NonMoving;
    public Rigidbody2D BulletRB;
    
    public float BulletSpeed;
    public float Distance;
    public int BulletDamage;


    private Vector3 MPos;

    void Start()
    {
        GetComponent<BoxCollider2D>().enabled = true;

        BulletRB = GetComponent<Rigidbody2D>();

        if (!NonMoving)
        {
            MPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 direction = MPos - transform.position;
            Vector3 rotation = transform.position = MPos;

            BulletRB.velocity = new Vector2(direction.x, direction.y).normalized * BulletSpeed;
            float BRot = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, BRot + 90);

            StartCoroutine(BulletTimer());
        }


    }


    private IEnumerator BulletTimer()
    {
        yield return new WaitForSecondsRealtime(5);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Enemy"))
        {
            Destroy(gameObject);

            EnemyStats EScript = other.transform.GetComponent<EnemyStats>();

            EScript.TakeDamage(BulletDamage);
        }
    }
}
