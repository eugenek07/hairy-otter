using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform[] spawnLocations;
    public float spawnTime;
    public GameObject monsterPrefab;

    void Start()
    {
        Debug.Log("spawning..."); 
        InvokeRepeating("Spawn", 0, spawnTime);
    }

    public void Spawn()
    {
        Transform spawn = spawnLocations[Random.Range(0, spawnLocations.Length)];
        GameObject monster = Instantiate(monsterPrefab, spawn.position, spawn.rotation);
    }
}
