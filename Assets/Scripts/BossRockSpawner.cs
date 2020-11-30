using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRockSpawner : MonoBehaviour
{
    public GameObject RockSpawnerNode;
    public void SpawnRocksAtSpawner()
    {
        RockManager.Main.Spawn(
                                RockSpawnerNode.transform.position - new Vector3(5f, 0.5f),
                                RockSpawnerNode.transform.position + new Vector3(5f, 0.5f),
                                3
                               );
    }
}
