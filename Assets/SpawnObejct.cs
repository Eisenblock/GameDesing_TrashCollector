using UnityEngine;

public class SpawnObejct : MonoBehaviour
{
    public GameObject trashPrefab;          // Prefab, das gespawnt wird
    public float spawnInterval = 2f;        // Alle wie viele Sekunden?
    public float spawnY = 10f;               // Fester Y-Wert
    public float minX = -8f, maxX = 8f;     // Bereich für X-Wert


    void Start()
    {
        InvokeRepeating(nameof(SpawnTrash), 0f, spawnInterval);
    }

    void SpawnTrash()
    {
        float randomX = Random.Range(minX, maxX);
        float randomForTrash = Random.Range(0, 3); // muss bis 3 gehen, sonst kommt nie 2
        Vector2 spawnPos = new Vector2(randomX, spawnY);

        GameObject spawnedTrash = Instantiate(trashPrefab, spawnPos, Quaternion.identity);

        if (randomForTrash == 0)
        {
            spawnedTrash.tag = "Yellow";
            spawnedTrash.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else if (randomForTrash == 1)
        {
            spawnedTrash.tag = "Blue";
            spawnedTrash.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (randomForTrash == 2)
        {
            spawnedTrash.tag = "Grün";
            spawnedTrash.GetComponent<SpriteRenderer>().color = Color.green;
        }
    }
}
