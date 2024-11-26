using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D PlrRB;
    public PlayerWeaponSelect PlayerWeapons;

    [Header("Movement Variables")]
    public float MoveSpeed = 5f;
    public float DashDistance = 7f;

    private float HozMove;
    private float VertMove;
    private float ObjRot;

    private Vector3 aimPoint;
    public Vector3 direction;

    private bool CanDash = true;
    private float currentCoolTime;
    private float currentDashTime;
    private float startCashTime = 1f;

    void Start()
    {
        PlrRB = GetComponent<Rigidbody2D>();
        CanDash = true;
    }

    void Update()
    {
        HozMove = Input.GetAxisRaw("Horizontal");
        VertMove = Input.GetAxisRaw("Vertical");

        CharacterMove();

        GunPointToMouse();
    }

    private void CharacterMove()
    {
        var PlrMovementDir = new Vector2(HozMove, VertMove).normalized;

        PlrRB.velocity = new Vector2(PlrMovementDir.x * MoveSpeed, PlrMovementDir.y * MoveSpeed);
    }

    private void GunPointToMouse()
    {
        aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = aimPoint - transform.position;

        ObjRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        PlrRB.rotation = ObjRot;

        if (CanDash && Input.GetKeyDown(KeyCode.LeftShift))
        {
            StartCoroutine(DashAbility(direction));
        }
    }

    private IEnumerator DashAbility(Vector2 DashDirection)
    {
        gameObject.GetComponent<Collider2D>().enabled = false;
        CanDash = false;
        currentCoolTime = startCashTime;

        while (currentDashTime > 0f)
        {
            Debug.Log("Try the Bananas Darling They're Dashing");

            currentDashTime -= Time.deltaTime;

            PlrRB.velocity = DashDirection * DashDistance;

            yield return null;
        }

        PlrRB.velocity = new Vector2(0f, 0f);

        gameObject.GetComponent<Collider2D>().enabled = true;
        CanDash = true;

    }
}
