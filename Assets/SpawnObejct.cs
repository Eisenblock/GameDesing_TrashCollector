using UnityEngine;

public class SpawnObejct : MonoBehaviour
{
    public GameObject trashPrefab;          // Prefab, das gespawnt wird
    public float spawnInterval = 2f;        // Alle wie viele Sekunden?
    public float spawnY = 10f;               // Fester Y-Wert
    public float minX = -8f, maxX = 8f;     // Bereich für X-Wert
    private float timer = 0;
    private float time_Checkpoints = 30;
    public float gravityScale = 0.1f;
    private int count_Dif = 0;
    private bool start_Asteroid = false;

    void Start()
    {
        InvokeRepeating(nameof(SpawnTrash), 0f, spawnInterval);
    }

    private void Update()
    {
        timer = Time.time;
        if (timer > time_Checkpoints)
        {
            gravityScale = gravityScale + 0.05f;
            time_Checkpoints = time_Checkpoints * 2;
            count_Dif += 1;
            if (count_Dif > 1)
            {
                start_Asteroid = true;
            }
        }
    }

    void SpawnTrash()
    {
        float randomX = Random.Range(minX, maxX);
        float randomForTrash = 0;
        if (!start_Asteroid) 
        {
            randomForTrash = Random.Range(0, 3);
        }
        else
        {
            randomForTrash = Random.Range(0, 4);
        }
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        GameObject spawnedTrash = Instantiate(trashPrefab, spawnPos, Quaternion.identity);

        if (randomForTrash == 0)
        {
            spawnedTrash.tag = "Yellow";
            spawnedTrash.GetComponent<SpriteRenderer>().color = Color.yellow;
            Rigidbody2D rb = spawnedTrash.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = gravityScale;
            }
        }
        else if (randomForTrash == 1)
        {
            spawnedTrash.tag = "Blue";
            spawnedTrash.GetComponent<SpriteRenderer>().color = Color.blue;
            Rigidbody2D rb = spawnedTrash.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = gravityScale;
            }
        }
        else if (randomForTrash == 2)
        {
            spawnedTrash.tag = "Grün";
            spawnedTrash.GetComponent<SpriteRenderer>().color = Color.green;
            Rigidbody2D rb = spawnedTrash.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = gravityScale;
            }
        }
        else if (randomForTrash == 3)
        {
            spawnedTrash.tag = "asteroid";
            spawnedTrash.GetComponent<SpriteRenderer>().color = Color.gray;
            Rigidbody2D rb = spawnedTrash.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                rb.gravityScale = gravityScale;
            }
        }
    }
}
