using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactersSpawner : MonoBehaviour
{
    [SerializeField] private GameObject character;
    [SerializeField] private int xRange;
    [SerializeField] private int zRange;
    [SerializeField] private int amount;

    private void Start()
    {
        for (int i = 0; i < amount; i++)
        {
            Spawn();
        }
    }

    void Spawn()
    {
        int spawnPointX = Random.Range(-xRange, xRange);
        int spawnPointZ = Random.Range(-zRange, zRange);
        Vector3 spawnPosition = new Vector3(spawnPointX, 0.5f, spawnPointZ);

        Instantiate(character, spawnPosition, Quaternion.identity);
    }
}
