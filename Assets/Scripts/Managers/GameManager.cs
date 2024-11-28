using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int PlayerPoints;
    public Transform PlayerTransform;
    public Transform SpawnPoint;

    public TextMeshPro PlayerPointText;

    private void Awake()
    {
        PlayerPoints = 0;

        PlayerTransform.position = SpawnPoint.position;
    }

    public void Update()
    {
        PlayerPointText.text = "Points:" + PlayerPoints;

        if(PlayerPoints <= 0)
        {
            PlayerPoints = 0;
        }
    }


    public void Respawn()
    {
        if(PlayerPoints > 0)
        {
            PlayerPoints = PlayerPoints - 500;
        }

        PlayerTransform.position = SpawnPoint.position;
    }
}
