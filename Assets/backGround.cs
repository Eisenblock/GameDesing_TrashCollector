using UnityEngine;

public class backGround : MonoBehaviour
{
    private float speed = 2f;       // Normale Geschwindigkeit
    private float speedUpAmount = 5f; // Geschwindigkeit beim SpeedUp
    private float durationSpeed_Up = 0f;
    private bool isSpeedUp = false;

    void Update()
    {
        // Hintergrund bewegen (nach unten z.B.)
        float currentSpeed = isSpeedUp ? speedUpAmount : speed;
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);

        // Optional: SpeedUp mit Tastendruck testen
        // if (Input.GetKeyDown(KeyCode.Space)) SpeedUp();
    }

    public void SpeedUp()
    {
        if (!isSpeedUp)
        {
            isSpeedUp = true;
            durationSpeed_Up = 5f;
            Invoke("ResetSpeedUp", durationSpeed_Up);
            Debug.Log("Speed erhöht auf: " + speedUpAmount);

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

    public void ResetSpeedUp()
    {
        isSpeedUp = false;
        Debug.Log("Speed zurückgesetzt auf: " + speed);
    }
}
