using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Teleportation : MonoBehaviour
{
    [SerializeField] private Transform targetTeleport;
    //[SerializeField] private Transform enemytargetTeleport;
    private bool isTeleporting = false;
    private Player player;
    private UIManager uiManager;

    private void Awake()
    {
        player = FindObjectOfType<Player>();
        uiManager = FindObjectOfType<UIManager>();
    }

    private void Start()
    {
        uiManager.button.interactable = false;
        isTeleporting = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C) && isTeleporting) 
        {
            TeleportPlayer();       
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("At first trigger " + other.gameObject.name);
        if (other.gameObject.name == "Player")
        {
            uiManager.button.interactable = true;
            Debug.Log("Interactable" + other.gameObject.name);
            isTeleporting = true;
        }
        /*else
        {
            uiManager.button.interactable = true;
            Debug.Log("Triggered" + other.gameObject.name);
            other.transform.position = enemytargetTeleport.position;
        }*/
    }

    private void TeleportPlayer()
    {
            Debug.Log("Input Key Pressed");
            player.characterController.enabled = false;
            Debug.Log("Teleporting" + player.gameObject.name);
            player.transform.position = targetTeleport.position;
            player.characterController.enabled = true;
            uiManager.button.interactable = false;        
    }

    private void OnTriggerExit(Collider other)
    {
        uiManager.button.interactable = false;
        isTeleporting = false;
    }

}

