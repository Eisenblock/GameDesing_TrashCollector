using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    public float fallSpeed = 3f; // Standard-Fallgeschwindigkeit, wird vom Spawner überschrieben

    void Update()
    {
        // Einfache Bewegung nach unten
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime, Space.World);

        // Optional: Zerstören, wenn es aus dem Bildschirm fällt (falls kein DeleteObject unten)
        if (transform.position.y < -10f) // Wert anpassen
        {
            Destroy(gameObject);
        }
    }
}