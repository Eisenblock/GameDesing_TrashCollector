using UnityEngine;

public class SpawnObejct : MonoBehaviour
{
    public GameObject[] templatePrefabs;
    public GameObject[] templatelvl1;
    public GameObject[] templatelvl2;
    public float spawnInterval = 80f; // z.B. alle 5 Sekunden ein Objekt
    public float speedIncreaseInterval = 10f; // alle 20 Sekunden schneller
    public float fallSpeed = 1f;
    private bool lvl1 = false;
    private bool lvl2 = false;
    private float spawnTimer = 80f;
    private float speedTimer = 0f;
    private int lastIndex = -1;
    private int countLvl = 0;
    public GameObject[] prefabsPickUPsToSpawn;
    public float spawnPickUpsInterval = 10f;
    private float pickUpSpawnTimer = 0f;
    private int countPickUP = 0;


    void Update()
    {
        spawnTimer += Time.deltaTime;
        Debug.Log($"SpawnTimer: {spawnTimer:F2} / {spawnInterval}");
        Debug.Log($"Level: {countLvl}, lvl1: {lvl1}, lvl2: {lvl2}");
        speedTimer += Time.deltaTime;

        pickUpSpawnTimer += Time.deltaTime;
        if (pickUpSpawnTimer >= spawnPickUpsInterval)
        {
            pickUpSpawnTimer = 0f;
            SpawnPiclUps();
        }

        // Geschwindigkeit alle 20s erhöhen
        if (speedTimer >= speedIncreaseInterval && countLvl <= 1)
        {
            switch (countLvl)
            {
                case 0:
                    lvl1 = true;
                    lvl2 = false;
                    break;
                case 1:
                    lvl1 = false;
                    lvl2 = true;
                    break;
                default:
                    lvl1 = false;
                    lvl2 = false;
                    break;
            }
            countLvl ++;
            speedTimer = 0;
        }

        if (!lvl1 && !lvl2)
        {
            SpawnTemplates(templatePrefabs);
        }

        if (lvl1 && !lvl2)
        {
            SpawnTemplates(templatelvl1);
        }

        if (!lvl1 && lvl2)
        {
            SpawnTemplates(templatelvl2);
        }
    }

    void SpawnTemplates(GameObject[] reftemps)
    {
        // Objekt spawnen
        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;

            if (reftemps == null || reftemps.Length == 0)
            {
                Debug.LogWarning("Kein Prefab im Array!");
                return;
            }

            int randomIndex;
            do
            {
                randomIndex = Random.Range(0, reftemps.Length);
            } while (templatePrefabs.Length > 1 && randomIndex == lastIndex);

            lastIndex = randomIndex;
            GameObject prefabToSpawn = reftemps[randomIndex];

            GameObject instance = Instantiate(prefabToSpawn, new Vector3(15, 0, 0), Quaternion.identity);

            // Fallgeschwindigkeit setzen
            TrashMovement trash = instance.GetComponent<TrashMovement>();
            if (trash != null)
            {
                trash.fallSpeed = fallSpeed;
            }
        }
    }

    void SpawnPiclUps()
    {
        if (prefabsPickUPsToSpawn == null || prefabsPickUPsToSpawn.Length == 0)
        {
            Debug.LogWarning("Keine PickUp-Prefabs vorhanden!");
            return;
        }

        int randomIndex = Random.Range(0, prefabsPickUPsToSpawn.Length);
        GameObject prefabToSpawn = prefabsPickUPsToSpawn[countPickUP];

        float randomX = Random.Range(-8f, 8f);
        Vector3 spawnPosition = new Vector3(randomX, 20f, 0f); // ggf. andere Y-Position

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        Debug.Log($"PickUp '{prefabToSpawn.name}' gespawnt bei X={randomX:F2}");
        countPickUP += 1;
        if (countPickUP >= 2)
        {
            countPickUP = 0;
        }
    }


}
