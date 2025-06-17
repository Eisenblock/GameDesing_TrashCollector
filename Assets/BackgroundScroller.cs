using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 2f;
    public float resetY = -20f;
    public float startY = 20f;

    void Update()
    {
        // Nach unten bewegen
        transform.Translate(Vector2.down * scrollSpeed * Time.deltaTime);

        // Wenn zu weit unten: wieder nach oben setzen
        if (transform.position.y <= resetY)
        {
            transform.position = new Vector3(transform.position.x, startY, transform.position.z);
        }
    }
}
