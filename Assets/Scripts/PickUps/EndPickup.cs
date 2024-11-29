using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPickup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            GameManager.Instance.WinGame();
        }
    }
}
