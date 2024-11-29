using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickUp : MonoBehaviour
{
    public float RespawnTime = 10;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            PlayerStats PlayerScript;
            PlayerScript = collision.transform.GetComponent<PlayerStats>();

            if (PlayerScript.CurrentHealth == 100)
            {
                PlayerScript.CurrentHealth = 100;
            }
            else
            {
                PlayerScript.CurrentHealth = PlayerScript.CurrentHealth + 50;
            }

            gameObject.SetActive(false);
        }
    }

    /*
    private IEnumerator RespawnHealthDrop()
    {
        yield return new WaitForSecondsRealtime(RespawnTime);

        gameObject.SetActive(true);
    }
    */
}
