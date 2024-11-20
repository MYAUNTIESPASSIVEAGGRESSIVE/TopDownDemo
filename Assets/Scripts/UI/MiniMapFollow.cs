using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapFollow : MonoBehaviour
{
    public Transform Player;

    void LateUpdate()
    {
        Vector3 NewPos = Player.position;
        NewPos.y = transform.position.y;
        transform.position = NewPos;

        transform.rotation = Quaternion.Euler(90f, Player.eulerAngles.y, 0f);
    }
}
