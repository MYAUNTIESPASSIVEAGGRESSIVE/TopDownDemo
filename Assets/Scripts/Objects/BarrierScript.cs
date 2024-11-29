using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BarrierScript : MonoBehaviour
{
    public TMP_Text InteractText;
    public GameObject BarrierObject;
    public int Cost;
    private bool InTrigger;

    private void Start()
    {
        InteractText.text = "Press 'E' To Open";
    }

    private void Update()
    {
        if(InTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if(GameManager.Instance.PlayerPoints >= Cost)
            {
                InteractText.gameObject.SetActive(false);
                GameManager.Instance.PlayerPoints -= Cost;
                Destroy(gameObject);
            }
            else
            {
                InteractText.text = "Not Enough Points!";
                StartCoroutine(TextTime());
            }
        }
    }

    private IEnumerator TextTime()
    {
        yield return new WaitForSecondsRealtime(1);
        InteractText.text = "Press 'E' To Open";
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            InteractText.gameObject.SetActive(true);
            InTrigger = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            InteractText.gameObject.SetActive(false);
            InTrigger = false;
        }
    }
}
