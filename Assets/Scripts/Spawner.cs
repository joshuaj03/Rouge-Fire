using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] GameObject enemyLvl1;
    [SerializeField] GameObject enemyLvl2;
    [SerializeField] GameObject enemyLvl3;
    [SerializeField] Transform location1;
    [SerializeField] Transform location2;
    [SerializeField] private int spawnNumberLvl1;
    [SerializeField] private int spawnNumberLvl2;
    [SerializeField] private int spawnNumberLvl3;

    private Gun gun;

    private void Start()
    {
        SpawnEnemyLvl1(spawnNumberLvl1);
        SpawnEnemyLvl2(spawnNumberLvl2);
        SpawnEnemyLvl3(spawnNumberLvl3);
    }
    private void SpawnEnemyLvl1(int value)
    {
        //gun.damage = 50;
        for(int i=0;i<value;i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(location1.position.x, location2.position.x), 10, Random.Range(location1.position.z, location2.position.z));
            Instantiate(enemyLvl1, randomLocation,Quaternion.identity);          

        }
    }

    private void SpawnEnemyLvl2(int value)
    {
        //gun.damage = 25;
        for (int i = 0; i < value; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(location1.position.x, location2.position.x), 10, Random.Range(location1.position.z, location2.position.z));
            Instantiate(enemyLvl2, randomLocation, Quaternion.identity);
        }
    }

    private void SpawnEnemyLvl3(int value)
    {
        //gun.damage = 10;
        for (int i = 0; i < value; i++)
        {
            Vector3 randomLocation = new Vector3(Random.Range(location1.position.x, location2.position.x), 10, Random.Range(location1.position.z, location2.position.z));
            Instantiate(enemyLvl3, randomLocation, Quaternion.identity);
        }
    }
}
