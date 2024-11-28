using UnityEngine;

public class EnemyStats : EntityStats
{
    [Header("Enemy Variables")]
    public Rigidbody2D EnemyRB;
    public float EnemyDamage = 15f;
    public float EnemySpeed = 5f;
    public float EnemyAttackRange = 1f;
    public float EnemyDetectionRange = 10f;
    public bool Converted;

    public GameObject Target;
    //public GameManager GameManager;

    public AudioClip[] EnemyHitSounds;
    public AudioSource EnemySource;
    //public GameObject EnemyWeapon;

    private float distancetoTarget;
    //private bool attackingTarget;

    protected override void Start()
    {
        base.Start();

        Target = GameObject.FindGameObjectWithTag("Player");

        Converted = false;

        EnemyRB = GetComponent<Rigidbody2D>();

        //attackingTarget = false;
    }

    private void FixedUpdate()
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
        if(Vector2.Distance(transform.position, Target.transform.position) > EnemyAttackRange)
        {
            EnemyRB.AddForce(transform.up * EnemySpeed, ForceMode2D.Force);
        }

        if (Vector2.Distance(transform.position, Target.transform.position) < EnemyAttackRange) 
        {
            EnemyRB.velocity = Vector2.zero;
        }
    }

    public override void TakeDamage(float Dmg, bool GoryDeath)
    {
        if (IsDead) return;

        base.TakeDamage(Dmg, GoryDeath);

        EnemySource.PlayOneShot(EnemyHitSounds[Random.Range(0, EnemyHitSounds.Length)]);

        //GameManagerScript.PlayerPoints = GameManagerScript.PlayerPoints + Random.Range(10, 20);
    }

    protected override void EntityDeath(bool GoryDeath)
    {
        if (IsDead) return;

        CurrentHealth = 0;

        IsDead = true;

        //GameManagerScript.PlayerPoints = GameManagerScript.PlayerPoints + Random.Range(100, 175);

        Destroy(gameObject);

        if (GoryDeath)
        {
            GoreDeathParticle.Play();
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void EnemyConversion()
    {
        Converted = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.transform.CompareTag("Player"))
        {
            collision.transform.GetComponent<PlayerStats>().TakeDamage(EnemyDamage, false);
        }

        if(collision.transform.CompareTag("Enemy") && Converted)
        {
            collision.transform.GetComponent<EnemyStats>().TakeDamage(EnemyDamage, false);
        }
    }

}
