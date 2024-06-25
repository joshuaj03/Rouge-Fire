using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.HighDefinition;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform rayOrigin;
    [SerializeField] private float maxDistance;
    [SerializeField] private LayerMask layerMask;
    [SerializeField] public int fullClip;
    [SerializeField] public int maxAmmo;
    private int currentAmmo;
    UIManager uiManager;
    [SerializeField] public int damage;
    private Player player;
    [SerializeField] private AudioSource fire;
    private Animator animatorGun;
    private bool isFire = false;
    [SerializeField] private AudioSource reload;
    private GameObject enemyLvl;
    public GameObject EnemyLvl
    {
        get { return enemyLvl; }
        private set { enemyLvl = value; }
    }

    private void Awake()
    {
        currentAmmo = maxAmmo;
        //animatorGun = GetComponent<Animator>();
        uiManager = FindObjectOfType<UIManager>();
        player = FindObjectOfType<Player>();
        animatorGun = player.animator;
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !player.isDead)
        {            
            Shoot();
            //animatorGun.Play("Fire");
            reduceAmmo();
            //ReloadAmmo();            
        }

        Debug.Log("Current Ammo at Update" + currentAmmo);
        if (Input.GetKeyDown(KeyCode.R) && (!player.isDead) && (fullClip > 0))
        {
            reload.Play();
            animatorGun.Play("Reload");
            int bullet = maxAmmo - currentAmmo;
            currentAmmo += bullet;
            fullClip -= bullet;
            uiManager.OnHit(currentAmmo);
            //fullClip--;
            uiManager.OnReload(fullClip);
        }    
        

        Debug.DrawRay(rayOrigin.position, rayOrigin.forward * maxDistance, Color.red);
    }

    private void Shoot()
    {
        RaycastHit raycastHit;
        if (currentAmmo >= 1)
        {
            fire.Play();
            animatorGun.Play("Fire");
            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out raycastHit, maxDistance, layerMask))
            {
                Debug.Log("Enemy Hit" + raycastHit.collider.gameObject);
                raycastHit.collider.gameObject.GetComponent<Enemy>().ReduceHealth(damage);
                enemyLvl = raycastHit.collider.gameObject;
            }
        }
    }

    private void reduceAmmo()
    {
        if (currentAmmo >= 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                currentAmmo--;
                uiManager.OnHit(currentAmmo);
                Debug.Log(currentAmmo);
            }
        }           
    }
}
