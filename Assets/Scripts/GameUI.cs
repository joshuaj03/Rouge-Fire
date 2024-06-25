using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;
using TMPro;
using System.Runtime.InteropServices;
using Unity.VisualScripting;

public class GameUI : MonoBehaviour
{
    [Header("Script Referencing")]
    [SerializeField] private GameObject mainGame;
    [SerializeField] private Player player;
    private Enemy enemy;
    private bool mainGameActive = false;
    private bool isMenuActive = true;
    //private bool isResume = true; ///
    private bool isStart = false;
    private GameUIEnable gameUIEnable;


    //[SerializeField] Canvas canvas;

    //First Page
    [Header("Front Page")]
    [SerializeField] GameObject frontPage;
    [SerializeField] Button startB;
    [SerializeField] Button settings;
    [SerializeField] Button about;
    [SerializeField] Button exit;

    //Settings Page
    [Header("Settings")]
    //[SerializeField] GameObject settingsPage;
    [SerializeField] GameObject settingsInside;
    [SerializeField] Button playerB;
    [SerializeField] Button sound;    
    [SerializeField] Button window;
    //[SerializeField] Button graphics;
    [SerializeField] Button back1;


    //////////////////////////////////////////////////////////////////////////

    //Settings Inside
    //[Header("Settings Inside")]
    //[SerializeField] GameObject settingsInside;
    //[SerializeField] Button back2;

    //Player
    [Header("Player Settings")]
    [SerializeField] GameObject playerOptions;
    [SerializeField] Slider playerSpeed;

    //Audio
    [Header("Audio Settings")]
    [SerializeField] GameObject audioOptions;
    [SerializeField] Slider soundSlider;

    //Window
    [Header("Window Settings")]
    [SerializeField] GameObject windowOptions;
    [SerializeField] Toggle windowed;
    [SerializeField] Toggle fullScreen;

///////////////////////////////////////////////////////////////////////////

    //About
    [Header("About")]
    [SerializeField] GameObject aboutPage;
    [SerializeField] Button back3;

    //Exit
    [Header("Exit")]
    [SerializeField] GameObject exitPage;
    [SerializeField] Button yesExit;
    [SerializeField] Button noExit;

    //Pause
    [Header("Pause Menu")]
    [SerializeField] GameObject pauseMenu;
    [SerializeField] Button resume;
    [SerializeField] Button settingsPause;
    [SerializeField] Button exitGame;

    [SerializeField] public AudioSource backgroundMusic;
    private bool isAnyUIActive = false;

    private void Awake()
    {
        enemy = FindObjectOfType<Enemy>();
        gameUIEnable = FindObjectOfType<GameUIEnable>();
        Screen.fullScreen = true;
        //mainGame.SetActive(false);

        //Front Page
        startB.onClick.AddListener(StartButton);
        settings.onClick.AddListener(Settings);
        about.onClick.AddListener(About);
        exit.onClick.AddListener(ExitPage);

////////////////////////////////////////////////////////////////
        //Settings Page
        playerB.onClick.AddListener(PlayerButton);
        sound.onClick.AddListener(Sound);
        window.onClick.AddListener(Window);
        //graphics.onClick.AddListener(GraphicsButton);
        back1.onClick.AddListener(Back1);

        //Settings Inside
        //back2.onClick.AddListener(Back1);

//////////////////////////////////////////////////////////////

        //Window Page
        windowed.onValueChanged.AddListener(WindowMode);
        fullScreen.onValueChanged.AddListener(FullScreenMode);

        //About Page
        back3.onClick.AddListener(Back1);

        //Exit Page
        yesExit.onClick.AddListener(YesExit);
        noExit.onClick.AddListener(NoExit);

        //Pause Menu
        resume.onClick.AddListener(StartButton);
        settingsPause.onClick.AddListener(Settings);
        exitGame.onClick.AddListener(ExitPage);
    }

    private void Start()
    {
        //backgroundMusic.Play();
        mainGame.SetActive(false);
        frontPage.SetActive(true);
        pauseMenu.SetActive(false);
        settingsInside.SetActive(false);
        aboutPage.SetActive(false);
        exitPage.SetActive(false);
        isMenuActive = true;
        isStart = false;
    }
    private void Update()
    {
        //Player Speed Slider
        //enemy.enemySpeed += player.runSpeed - playerSpeed.value;
        player.runSpeed = playerSpeed.value;

        //Volume Slider
        AudioListener.volume = soundSlider.value;

        //////////////////////////////////////////////////////////////////////////////////////////////////////////
        if (!frontPage.activeSelf && !aboutPage.activeSelf && !exitPage.activeSelf && !settingsInside.activeSelf) //!settingsPage.activeSelf && 
        {
            Debug.Log("Check 3");
            if (!mainGame.activeSelf)
            {
                //Debug.Log("Playing BG 1");
                //backgroundMusic.Play();
                Debug.Log("Game Activated, UI Camera Disabled");
                isMenuActive = false;
                isStart = true;
                mainGame.SetActive(true);
                mainGameActive = true;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
        }
        else
        {
            Debug.Log("Check 1");
            mainGameActive = false;
            mainGame.SetActive(false);
            Debug.Log("Playing BG 2");
            backgroundMusic.Play();
            isMenuActive = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            isStart = false;


            if (frontPage.activeSelf)
            {
                Debug.Log("Front Page is active");
            }
            else if(settingsInside.activeSelf) 
            {
                Debug.Log("Settings Inside is active");
            }
            else if (aboutPage.activeSelf)
            {
                Debug.Log("About Page is active");
            }
            else if (exitPage.activeSelf)
            {
                Debug.Log("Exit Page is active");
            }
            else if (pauseMenu.activeSelf)
            {
                Debug.Log("Pause Menu is active");
            }
            else
            {
                Debug.Log("No UI element is active");
            }

        }
        //////////////////////////////////////////////////////////////////////

        if (isStart)
        {
            Debug.Log("Check 2");
            mainGame.SetActive(true);
        }
        else
        {
            mainGame.SetActive(false);
        }

        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (pauseMenu.activeSelf)
            {
                Debug.Log("Escape Key 2");
                pauseMenu.SetActive(false);
                //isResume = false;
            }
            else
            {
                Debug.Log("Escape Key 1");
                pauseMenu.SetActive(true);
                //isResume = true;
            }
        }

        /*bool wasAnyUIActive = isAnyUIActive; // Store the previous state of the UI elements

        // Check if any UI element is active
        isAnyUIActive = frontPage.activeSelf || settingsInside.activeSelf || aboutPage.activeSelf || exitPage.activeSelf || pauseMenu.activeSelf;

        // Play the background music if the first UI element becomes active
        if (isAnyUIActive && !wasAnyUIActive)
        {
            backgroundMusic.Play();
        }

        // Pause the background music if the last UI element becomes inactive
        if (!isAnyUIActive && wasAnyUIActive)
        {
            Debug.Log("Time to pause");
            //backgroundMusic.Pause();
        }*/

    }

    private void StartButton()
    {
        Debug.Log("Start Button Clicked");
        //backgroundMusic.mute = true;
        isStart = true;
        gameObject.GetComponent<Canvas>().enabled = false;
        //isResume = false;
        frontPage.SetActive(false);
        //settingsPage.SetActive(false);
        settingsInside.SetActive(false);
        aboutPage.SetActive(false);
        exitPage.SetActive(false);
    }
    private void Settings()
    {
        Debug.Log("Settings");
        isStart = false;
        //settingsPage.SetActive(true);
        frontPage.SetActive(false);
        settingsInside.SetActive(true);
        playerOptions.SetActive(false);
        audioOptions.SetActive(false);
        windowOptions.SetActive(false);
        exitPage.SetActive(false);
    }

    private void About()
    {
        isStart = false; 
        frontPage.SetActive(false);
        //settingsPage.SetActive(false);
        settingsInside.SetActive(false);
        aboutPage.SetActive(true);
        exitPage.SetActive(false);
    }

    private void ExitPage()
    {
        isStart = false;
        frontPage.SetActive(false);
        //settingsPage.SetActive(false);
        settingsInside.SetActive(false);
        aboutPage.SetActive(false);
        exitPage.SetActive(true);
    }

    private void PlayerButton()
    {
        Debug.Log("Player Button Pressed");
        isStart = false;
        settingsInside.SetActive(true);
        playerOptions.SetActive(true);
        audioOptions.SetActive(false);
        windowOptions.SetActive(false);
    }

    private void Sound()
    {
        Debug.Log("Audio Button Pressed");
        isStart = false;
        settingsInside.SetActive(true);
        audioOptions.SetActive(true);
        playerOptions.SetActive(false);        
        windowOptions.SetActive(false);
    }

    private void Window()
    {
        Debug.Log("Window Button Pressed");
        isStart = false;
        settingsInside.SetActive(true);
        windowOptions.SetActive(true);
        playerOptions.SetActive(false);
        audioOptions.SetActive(false);        
    }

    private void Back1()
    {
        frontPage.SetActive(true);
        //settingsPage.SetActive(false);
        settingsInside.SetActive(false);
        aboutPage.SetActive(false);
        exitPage.SetActive(false);
    }

    /*private void Back2()
    {
        frontPage.SetActive(true);
        //settingsPage.SetActive(true);
        settingsInside.SetActive(false);
        aboutPage.SetActive(false);
        exitPage.SetActive(false);
    }*/


    private void YesExit()
    {
        Application.Quit();
    }

    private void NoExit()
    {
        frontPage.SetActive(true);
        //settingsPage.SetActive(false);
        settingsInside.SetActive(false);        
        aboutPage.SetActive(false);
        exitPage.SetActive(false);
    }

    private void WindowMode(bool mode)
    {
        if (mode)
        {
            Screen.fullScreen = false;
            windowed.isOn = true;
            fullScreen.isOn = false;
        }
        else
        {
            Screen.fullScreen = true;
            fullScreen.isOn = true;
            windowed.isOn = false;
        }        
    }

    private void FullScreenMode(bool mode)
    {
        if (mode)
        {
            Screen.fullScreen = true;
            fullScreen.isOn = true;
            windowed.isOn = false;
        }
        else
        {
            Screen.fullScreen = false;
            fullScreen.isOn = false;
            windowed.isOn = true;
        }
    }
}


