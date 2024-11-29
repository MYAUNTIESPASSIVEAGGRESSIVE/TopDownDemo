using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get { return _instance; } }
    private static GameManager _instance;

    public int PlayerPoints;
    public Transform PlayerTransform;
    public Transform SpawnPoint;

    public TMP_Text PlayerPointText;

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
        }

        PlayerTransform.position = SpawnPoint.position;

    }

    private void Start()
    {
        PlayerPoints = 0;

        PlayerPointText.text = "Points: 0";
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

    public void WinGame()
    {
        SceneManager.LoadSceneAsync("Credits");
    }
}
