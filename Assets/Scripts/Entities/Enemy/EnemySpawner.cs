using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject EnemyPrefab;
    //timer floats
    public float SpawnTime;
    private float CurSpawnTime;

    private int NodeAmount;

    public List<GameObject> SpawnPoints = new List<GameObject>();

    private void Start()
    {
        foreach(Transform t in transform)
        {
            SpawnPoints.Add(t.gameObject);
        }
    }

    private void Update()
    {
        CurSpawnTime += Time.deltaTime;

        if (CurSpawnTime > SpawnTime)
        {
            CurSpawnTime = 0;

            Vector3 SpawnLocation = Vector3.zero;
            int RandomLocation = Random.Range(0, SpawnPoints.Count);
            SpawnLocation = SpawnPoints[RandomLocation].transform.position;

            GameObject NewEnemy = Instantiate(EnemyPrefab, SpawnLocation, Quaternion.identity);
        }
    }
}
