using UnityEngine;

public class SpawnObejct : MonoBehaviour
{
    public GameObject[] templatePrefabs;
    public float spawnInterval = 80f; // z.B. alle 5 Sekunden ein Objekt
    public float speedIncreaseInterval = 30f; // alle 20 Sekunden schneller
    public float fallSpeed = 4f;

    private float spawnTimer = 80f;
    private float speedTimer = 0f;
    private int lastIndex = -1;

    void Update()
    {
        spawnTimer += Time.deltaTime;
        Debug.Log($"SpawnTimer: {spawnTimer:F2} / {spawnInterval}");
        speedTimer += Time.deltaTime;

        // Objekt spawnen
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            if (templatePrefabs == null || templatePrefabs.Length == 0)
            {
                Debug.LogWarning("Kein Prefab im Array!");
                return;
            }

            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, templatePrefabs.Length);
            } while (templatePrefabs.Length > 1 && randomIndex == lastIndex);

            lastIndex = randomIndex;
            GameObject prefabToSpawn = templatePrefabs[randomIndex];

            GameObject instance = Instantiate(prefabToSpawn, new Vector3(15, 0, 0), Quaternion.identity);

            // Fallgeschwindigkeit setzen
            TrashMovement trash = instance.GetComponent<TrashMovement>();
            if (trash != null)
            {
                trash.fallSpeed = fallSpeed;
            }
        }

        // Geschwindigkeit alle 20s erhöhen
       /*if (speedTimer >= speedIncreaseInterval)
        {
            speedTimer = 0f;
            fallSpeed += 1f;
            spawnInterval -= 4;
            Debug.Log($"Fallgeschwindigkeit erhöht auf: {fallSpeed}");
        }*/
    }
}
