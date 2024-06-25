using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameUIEnable : MonoBehaviour
{
    [SerializeField] private GameObject canvas;
    private GameUI gameUI;

    private void Awake()
    {
        gameUI = FindObjectOfType<GameUI>();
    }

   public void CanvasEnable()
    {
        canvas.SetActive(true);    
    }
    public void CanvasDisable()
    {
        canvas.SetActive(false);
    }


}
