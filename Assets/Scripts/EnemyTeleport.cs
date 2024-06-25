using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTeleport : MonoBehaviour
{
    [SerializeField] private Transform enemyTeleport;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "Player")
        {
            Debug.Log("Enemy Teleported " + other.gameObject.name);
            other.transform.position = enemyTeleport.position;
        }
    }
}
