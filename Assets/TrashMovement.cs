using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    public float fallSpeed = 2f; // Geschwindigkeit in Einheiten pro Sekunde

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
    }
}
