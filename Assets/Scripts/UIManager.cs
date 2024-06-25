using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI startText;
    [SerializeField] Slider healthBar;
    [SerializeField] TextMeshProUGUI ammoInfo;
    [SerializeField] TextMeshProUGUI reload;
    private Gun gun;
    //private int ammo;
    public Button button;
    [SerializeField] private AudioSource dead;
    [SerializeField] private AudioSource end;
    [SerializeField] public TextMeshProUGUI scoreBox;
    [SerializeField] private GameObject enemyLvl1;
    [SerializeField] private GameObject enemyLvl2;
    [SerializeField] private GameObject enemyLvl3;
    [SerializeField] private GameObject startTextObject;
    private GameUI gameUI;
    private int MainScore = 0;
    private Player player;

    public void Awake()
    {
        gun = FindAnyObjectByType<Gun>();
        ammoInfo.text = gun.maxAmmo.ToString();
        reload.text = gun.fullClip.ToString();
        button = FindAnyObjectByType<Button>();
        gameUI = FindAnyObjectByType<GameUI>();
        player = FindObjectOfType<Player>();
    }

    private void Start()
    {
        scoreBox.text = "";
    }
    private void Update()
    {
        if(Input.anyKeyDown)
        {
            
            startTextObject.SetActive(false);
        }
    }

    public void Score (int score)
    {
        MainScore += score;
        /*if (gun.EnemyLvl.name == enemyLvl1.name) {
            score += 20;
        }
        else if(gun.EnemyLvl.name == enemyLvl2.name)
        {
            score += 30;
        }
        else if(gun.EnemyLvl.name == enemyLvl3.name)
        {
            score += 50;
        }*/
        scoreBox.text = MainScore.ToString();
    }

    public void OnHit(int currentAmmo)
    {
        ammoInfo.text = currentAmmo.ToString();
    }

    public void OnReload(int reloadAmmo)
    {
        reload.text = reloadAmmo.ToString();
    }

    public void OnHealthReduced(int currentHealth)
    {
        Debug.Log(currentHealth);
        healthBar.value = currentHealth;

        if ( currentHealth == 0 )
        {
            gameUI.backgroundMusic.mute = true;
            dead.Play();
            startText.text = "YOU ARE DEAD";
            for (int i = 0; i < 4; i++)
            {
                if (i == 3)
                {
                    player.hurt.mute = true;
                    end.Play();
                }
            }
        }
    }

}
