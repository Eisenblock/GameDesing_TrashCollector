using UnityEngine;

public class DeleteObject : MonoBehaviour
{
    private PlayerMovement playerMovement; // Referenz zum PlayerMovement Skript
    public float energyLossOnMiss = 1f;   // Wie viel Energie abgezogen 

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();

        if (playerMovement == null)
        {
            Debug.LogError("DeleteObject konnte PlayerMovement nicht in der Szene finden!");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        string objectTag = collidedObject.tag;

        // Prüfen, ob es ein farbiges Müllobjekt ist
        if (objectTag == "Yellow" || objectTag == "Blue" || objectTag == "Green")
        {
            if (playerMovement != null)
            {
                playerMovement.DeductEnergyForMissedItem(energyLossOnMiss);
            }
            else
            {
                // Fallback, falls PlayerMovement nicht gefunden wurde (sollte aber nicht passieren, wenn im Start() gefunden)
                Debug.LogWarning("PlayerMovement Referenz in DeleteObject fehlt, Energie für verpasstes Item nicht abgezogen.");
            }
        }

        Destroy(collidedObject);
    }
}