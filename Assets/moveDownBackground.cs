using UnityEngine;

public class moveDownBackground : MonoBehaviour
{
    [Header("Bewegungsgeschwindigkeit in Einheiten/Sekunde")]
    public float speed = 5f;
    private float timerDeath = 60f;
    private float timer = 0;
    void Update()
    {
        // Bewegung nach unten entlang der Y-Achse (unabhängig von Framerate)
        transform.position += Vector3.down * speed * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer >= timerDeath)
        {
            Destroy(this);
        }
    }
}
