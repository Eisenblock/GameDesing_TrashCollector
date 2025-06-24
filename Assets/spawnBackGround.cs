using UnityEngine;

public class spawnBackGround : MonoBehaviour
{
    [Header("Prefabs zum Spawnen")]
    public GameObject[] prefabs;

    [Header("Spawnposition")]
    public Transform spawnPoint;

    [Header("Intervall in Sekunden")]
    public float spawnInterval = 60f;

    private float timer;

    void Start()
    {
        // Starte mit vollem Timer, sodass sofort gespawnt wird
        timer = 0;
    }

    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0f)
        {
            SpawnRandomPrefab();
            timer = spawnInterval; // Timer zurücksetzen
        }
    }

    void SpawnRandomPrefab()
    {
        if (prefabs.Length == 0)
        {
            Debug.LogWarning("Keine Prefabs zugewiesen!");
            return;
        }

        int index = Random.Range(0, prefabs.Length);
        GameObject prefab = prefabs[index];

        if (prefab != null)
        {
            Instantiate(prefab, new Vector3(-58, 100, 0), Quaternion.identity);
            Debug.Log($"Spawned: {prefab.name}");
        }
    }
}
