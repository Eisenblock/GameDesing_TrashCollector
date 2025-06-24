using TMPro;
using UnityEngine;

public class SetUI_Values : MonoBehaviour
{

    public TMP_Text ScoreValue;
    GameManagerGlobal gm;
    private GameObject globalObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        globalObject = GameObject.FindWithTag("GlobalManager");
        gm = globalObject.GetComponent<GameManagerGlobal>();
    }

    // Update is called once per frame
    void Update()
    {
        ScoreValue.text = Mathf.FloorToInt(gm.scorePLayer).ToString();
    }
}
