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

    private float HozMove;
    private float VertMove;
    private float ObjRot;

    private Vector3 aimPoint;
    private Vector3 direction;

    void Start()
    {
        PlrRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HozMove = Input.GetAxisRaw("Horizontal");
        VertMove = Input.GetAxisRaw("Vertical");

        CharacterMove();

        GunPointToCam();
    }

    private void CharacterMove()
    {
        var PlrMovementDir = new Vector2(HozMove, VertMove).normalized;

        PlrRB.velocity = new Vector2(PlrMovementDir.x * MoveSpeed, PlrMovementDir.y * MoveSpeed);
    }

    private void GunPointToCam()
    {
        aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = aimPoint - transform.position;

        ObjRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, ObjRot);
    }
}
