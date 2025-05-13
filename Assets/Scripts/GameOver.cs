using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class GameOver : MonoBehaviour
{
    [SerializeField] TMP_Text myText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Start()
    {
        string newText = PlayerMovement.totalScore.ToString();
        myText.text = newText;
        
    }

}
