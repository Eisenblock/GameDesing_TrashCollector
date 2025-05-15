using DG.Tweening.Core.Easing;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GameManagerGlobal : MonoBehaviour
{
    public static GameManagerGlobal Instance;
    public int scorePLayer = 0;
    public bool gameOver = false;

    public void Update()
    {
        if (!gameOver)
        {
           scorePLayer += (int)(0.5f * Time.time); 
        }
        
    }

    private void Awake()
    {
        // Singleton-Pattern: Es soll nur ein GameManager existieren
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Überlebt Szenenwechsel
        }
        else
        {
            Destroy(gameObject); // Verhindert Duplikate
        }
    }

}
