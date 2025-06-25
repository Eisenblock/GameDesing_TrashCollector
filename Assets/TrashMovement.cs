using UnityEngine;

public class TrashMovement : MonoBehaviour
{
    public float fallSpeed = 1f; // Geschwindigkeit in Einheiten pro Sekunde
    private float timerDeath = 40f;
    private float timer = 0;

    void Update()
    {
        transform.Translate(Vector2.down * fallSpeed * Time.deltaTime);
        timer += Time.deltaTime;
        if (timer >= timerDeath)
        {
            Destroy(this);
        }
    }
}
