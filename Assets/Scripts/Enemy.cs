using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    //[SerializeField] public float enemySpeed;
    private Player player;
    private bool playerInsight = false;

    //Random Movement
    //private Vector3 resetPosition;
    private Vector3 randomPosition;
    //private Vector3 randomDirection;
    //private Vector3 randomDirectionContinuous = Random.insideUnitSphere;
    private bool isMovingRandomly = false;
    //private Vector3 randomLocation;



    [SerializeField] private Transform loc1;
    [SerializeField] private Transform loc2;

    //Health & Damage
    [SerializeField] private Slider healthBar;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;

    private UIManager uiManager;

    //private Rigidbody m_rigidbody; //Test

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        //enemySpeed = 5f;
        //navMeshAgent.speed = enemySpeed;
        player = FindObjectOfType<Player>();
        uiManager = FindObjectOfType<UIManager>();

        //resetPosition = transform.position;

        //m_rigidbody = GetComponent<Rigidbody>(); //Test
    }

    private void Start()
    {
        //enemySpeed = 5f;
        isMovingRandomly = true;
        MoveRandomly();
        //StartCoroutine(movingRandomly());
        //randomLocation = new Vector3(Random.Range(loc1.position.x, loc2.position.x), 10, Random.Range(loc1.position.z, loc2.position.z));

        currentHealth = maxHealth;
        healthBar.value = currentHealth;
    
        /*randomDirection = Random.insideUnitSphere;
        randomDirection.y = 0f;
        randomPosition = transform.position + randomDirection * 10f;

        navMeshAgent.SetDestination(randomPosition);*/
    }

    private void Update()
    {
        if(isMovingRandomly)
        {
            if(navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
            {
                MoveRandomly();
            }
        }

        //navMeshAgent.speed = enemySpeed;

        //randomDirectionContinuous.y = 0f;
        /*if (!playerInsight)
        {
            if(!isMovingRandomly)
            {
                isMovingRandomly = true;
                //randomLocation = new Vector3(Random.Range(loc1.position.x, loc2.position.x), 10, Random.Range(loc1.position.z, loc2.position.z));
                //navMeshAgent.SetDestination(randomLocation);
                StartCoroutine(movingRandomly());
            }

            //navMeshAgent.SetDestination(randomPosition); //OG code
            //navMeshAgent.SetDestination(randomDirectionContinuous * 10f);
            return;
        }*/

        if(Vector3.Distance(transform.position,player.transform.position) > 50)
        {
            playerInsight = false;
            //navMeshAgent.isStopped = true;
            
            return;
        }

        //navMeshAgent.isStopped = false;
        navMeshAgent.SetDestination(player.transform.position);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>())
        {
            playerInsight = true;
        }
    }

    private void MoveRandomly()
    {
        Vector3 randomLocation = new Vector3(Random.Range(loc1.position.x, loc2.position.x),transform.position.y, Random.Range(loc1.position.z, loc2.position.z));
        navMeshAgent.SetDestination(randomLocation);
    }

    /*private IEnumerator movingRandomly()
    {
        while(isMovingRandomly) 
        {
            randomDirection = Random.insideUnitSphere;
            randomDirection.y = 0f;
            randomPosition = transform.position + randomDirection * 1f;

            navMeshAgent.SetDestination(randomPosition);

            yield return new WaitForSeconds(Random.Range(1f, 2f));
        }
        
    }*/

    public void ReduceHealth(int damage)
    {
        currentHealth -= damage;
        healthBar.value = currentHealth;

        if(currentHealth <= 0)
        {
            //transform.rotation = Quaternion.Euler(0, 0, 90);
            //m_rigidbody.constraints = RigidbodyConstraints.FreezeAll; //Test
            int scoreVal = 5;
            uiManager.Score(scoreVal);
            Destroy(gameObject);
        }
    }
}

