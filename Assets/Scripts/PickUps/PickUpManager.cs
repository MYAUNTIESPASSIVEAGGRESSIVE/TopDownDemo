using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpManager : MonoBehaviour
{
    public List<GameObject> PickUps = new List<GameObject>();

    private void Start()
    {
        foreach (Transform t in transform)
        {
            PickUps.Add(t.gameObject);
        }
    }

    private void Update()
    {
        for(int i = 0; i < PickUps.Count; i++)
        {
            if (!PickUps[i].activeSelf)
            {
                float CurSpawnTime = 0;
                float SpawnTime = 3;

                CurSpawnTime += Time.deltaTime;

                if (CurSpawnTime > SpawnTime)
                {
                    PickUps[i].SetActive(true);

                    CurSpawnTime = 0;
                }
            }
        }
    }
}
