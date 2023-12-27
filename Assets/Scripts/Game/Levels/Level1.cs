using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level1 : MonoBehaviour
{
    [SerializeField] private Cat1x1 cat1Prefab;
    [SerializeField] private Cat1x2 cat2Prefab;
    [SerializeField] private Transform parent;

    public void Awake()
    {
        SpawnCats();
    }

    void SpawnCats()
    {
        var spawnedcat1 = Instantiate(cat1Prefab, new Vector3(8, 4), Quaternion.identity);
        var spawnedcat2 = Instantiate(cat2Prefab, new Vector3(6, 4), Quaternion.identity);
        spawnedcat1.transform.SetParent(parent);
        spawnedcat2.transform.SetParent(parent);
    }
}
