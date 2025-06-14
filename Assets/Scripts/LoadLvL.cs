using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLvL : MonoBehaviour
{
    GameManagerGlobal gm;
    SpawnObejct spawnOB;
    private GameObject globalObject;
   
    void Start()
    {
        globalObject = GameObject.FindWithTag("GlobalManager");
        gm = globalObject.GetComponent<GameManagerGlobal>();
    }
    public void LoadMainMenu()
    {
        if (gm.gameOver) 
        {
            Debug.Log("ResetSCore");
            gm.scorePLayer = 0;
            gm.gameOver = false;
        }
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }
}
