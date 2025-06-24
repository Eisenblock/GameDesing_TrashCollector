using UnityEngine;

public class moveDownBackground : MonoBehaviour
{
    [Header("Bewegungsgeschwindigkeit in Einheiten/Sekunde")]
    public float speed = 5f;
    private float timerDeath = 60f;
    private float timer = 0;
    private float timerSpeedUp = 0;
    private bool  fastTravelActive = false;
    void Update()
    {
        // Bewegung nach unten entlang der Y-Achse (unabhängig von Framerate)
        transform.position += Vector3.down * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= timerDeath)
        {
            Destroy(this);
        }

        if (fastTravelActive) 
        {
            timerSpeedUp -= Time.deltaTime;
            if (timerSpeedUp < 0)
            {
                fastTravelActive = false;
                speed = 0;
            }
        }
        
    }

    public void SpeedUp()
    {
        Debug.Log("fastTravel");
        if (!fastTravelActive)
        {
            fastTravelActive = true;
            timerSpeedUp = 3f;
            speed = 10f;


            // GlobalManager finden und PlayerScore erhöhen
            GameObject globalManager = GameObject.FindWithTag("GlobalManager");
            if (globalManager != null)
            {
                GameManagerGlobal managerScript = globalManager.GetComponent<GameManagerGlobal>();
                if (managerScript != null)
                {
                    managerScript.scorePLayer += 5000;
                    Debug.Log("PlayerScore um 5000 erhöht, neuer Wert: " + managerScript.scorePLayer);
                }
                else
                {
                    Debug.LogWarning("GlobalManagerScript nicht gefunden auf GlobalManager!");
                }
            }
            else
            {
                Debug.LogWarning("Kein GameObject mit Tag 'GlobalManager' gefunden!");
            }
        }
    }
}
