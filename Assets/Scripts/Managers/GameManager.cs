using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int PlayerPoints;
    public Transform PlayerTransform;
    public Transform SpawnPoint;

    private void Awake()
    {
        PlayerPoints = 0;

        PlayerTransform.position = SpawnPoint.position;
    }

    public void Respawn()
    {
        PlayerTransform.position = SpawnPoint.position;
    }
}
