using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D PlrRB;
    public PlayerWeaponSelect PlayerWeapons;
    public Canvas PauseMenu;
    public Canvas PlayerUI;

    [Header("Movement Variables")]
    public float MoveSpeed = 5f;
    private float ActiveMoveSpeed;
    public float DashDistance = 7f;
    public bool PlayerDead;

    private float HozMove;
    private float VertMove;
    private float ObjRot;

    private Vector3 aimPoint;
    public Vector3 direction;

    private float dashLength = 0.5f, DashCooldown = 1f;
    private float DashCounter;
    private float DashCoolCounter;
    private bool Dashing;

    void Start()
    {
        PlrRB = GetComponent<Rigidbody2D>();
        ActiveMoveSpeed = MoveSpeed;
    }

    void Update()
    {
        HozMove = Input.GetAxisRaw("Horizontal");
        VertMove = Input.GetAxisRaw("Vertical");

        CharacterMove();

        GunPointToMouse();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }

        if (PlayerDead)
        {
            PlayerUI.gameObject.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {

            if(DashCoolCounter <= 0 && DashCounter <= 0)
            {
                ActiveMoveSpeed = DashDistance;
                DashCounter = dashLength;
                Dashing = true;
            }

        }

        if (Dashing)
        {
            if (DashCounter > 0)
            {
                DashCounter -= Time.deltaTime;

                if (DashCounter <= 0)
                {
                    ActiveMoveSpeed = MoveSpeed;
                    DashCoolCounter = DashCooldown;
                }
            }

            if (DashCoolCounter > 0)
            {
                DashCoolCounter -= Time.deltaTime;
            }
        }
    }

    private void CharacterMove()
    {
        var PlrMovementDir = new Vector2(HozMove, VertMove).normalized;

        PlrRB.velocity = new Vector2(PlrMovementDir.x * MoveSpeed, PlrMovementDir.y * ActiveMoveSpeed);
    }

    private void GunPointToMouse()
    {
        aimPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = aimPoint - transform.position;

        ObjRot = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

        PlrRB.rotation = ObjRot;
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        PlayerUI.gameObject.SetActive(false);
        PauseMenu.gameObject.SetActive(true);
    }
}
