using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    [Header("References")]
    public Rigidbody2D PlrRB;

    [Header("Movement Variables")]
    public float MoveSpeed = 5f;

    private float HozMove;
    private float VertMove;


    void Start()
    {
        PlrRB = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HozMove = Input.GetAxisRaw("Horizontal");
        VertMove = Input.GetAxisRaw("Vertical");

        CharacterMove();
    }

    private void CharacterMove()
    {
        var PlrMovementDir = new Vector2(HozMove, VertMove).normalized;

        PlrRB.velocity = new Vector2(PlrMovementDir.x * MoveSpeed, PlrMovementDir.y * MoveSpeed);
    }
}
