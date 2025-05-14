using UnityEngine;
using System.Collections; // Notwendig für Coroutinen

public class SpawnObejct : MonoBehaviour
{
    public GameObject trashPrefab;
    public GameObject asteroidPrefab;

    [Header("Base Spawn Settings")]
    public float initialSpawnInterval = 2f;    // Basis-Intervall, das durch Schwierigkeit modifiziert wird
    public float baseMinSpawnDelayFactor = 0.75f; // Spawn frühestens nach currentEffectiveInterval * Factor
    public float baseMaxSpawnDelayFactor = 1.25f; // Spawn spätestens nach currentEffectiveInterval * Factor
    public float initialFallSpeed = 3f;
    public float spawnY = 10f;
    public float minX = -8f, maxX = 8f;

    [Header("X Position Randomization")]
    private float lastSpawnX = float.MinValue; // Speichert die X-Position des letzten Spawns
    public float minXDifference = 1.0f;       // Mindestabstand zur letzten X-Position (im Inspector einstellen)
    public int maxSpawnXAttempts = 5;         // Maximale Versuche, eine neue X-Position zu finden

    [Header("Difficulty Modifiers (per level)")]
    public float spawnIntervalDecrease = 0.25f; // Wie viel das Basis-Intervall pro Level reduziert wird
    public float fallSpeedIncrease = 0.5f;
    public float baseAsteroidChance = 0.0f;
    public float asteroidChanceIncrease = 0.1f;

    // Aktuelle dynamische Werte
    private float currentMinSpawnDelay;
    private float currentMaxSpawnDelay;
    private float currentFallSpeed;
    private float currentAsteroidChance;
    private Coroutine spawnCoroutine; // Um die Coroutine stoppen und neu starten zu können

    void Start()
    {
        UpdateDifficultyParameters(); // Initiale Parameter setzen
        // Starte die Spawn-Coroutine
        if (spawnCoroutine != null) StopCoroutine(spawnCoroutine);
        spawnCoroutine = StartCoroutine(SpawnLoop());
    }

    public void UpdateDifficultyParameters()
    {
        int difficulty = PlayerMovement.currentDifficultyLevel;

        // Berechne das effektive Basis-Intervall für das aktuelle Schwierigkeitslevel
        float currentEffectiveInterval = Mathf.Max(0.3f, initialSpawnInterval - (difficulty * spawnIntervalDecrease));

        // Berechne die min/max Delays basierend auf dem effektiven Intervall und den Faktoren
        currentMinSpawnDelay = Mathf.Max(0.1f, currentEffectiveInterval * baseMinSpawnDelayFactor);
        currentMaxSpawnDelay = Mathf.Max(0.2f, currentEffectiveInterval * baseMaxSpawnDelayFactor);
        // Sicherstellen, dass max immer größer als min ist (oder gleich, falls Faktoren sehr klein)
        if (currentMaxSpawnDelay <= currentMinSpawnDelay) currentMaxSpawnDelay = currentMinSpawnDelay + 0.1f;


        currentFallSpeed = initialFallSpeed + (difficulty * fallSpeedIncrease);
        currentAsteroidChance = Mathf.Clamp01(baseAsteroidChance + (difficulty * asteroidChanceIncrease));

        Debug.Log($"Difficulty {difficulty}: EffectiveInterval={currentEffectiveInterval:F2}, SpawnDelayRange=[{currentMinSpawnDelay:F2}-{currentMaxSpawnDelay:F2}], FallSpeed={currentFallSpeed}, AsteroidChance={currentAsteroidChance}");

        // Wenn die Coroutine bereits läuft (z.B. durch einen früheren Aufruf von Start oder eine frühere Schwierigkeitsänderung),
        // stoppe sie und starte sie neu, damit sie die neuen Delay-Zeiten verwendet.
        if (spawnCoroutine != null)
        {
            StopCoroutine(spawnCoroutine);
            spawnCoroutine = StartCoroutine(SpawnLoop());
        }
    }

    IEnumerator SpawnLoop()
    {
        // Kleine anfängliche Verzögerung, um zu verhindern, dass sofort beim Start gespawnt wird (optional)
        // yield return new WaitForSeconds(Random.Range(currentMinSpawnDelay, currentMaxSpawnDelay) * 0.5f); 

        while (true) // Endlosschleife, solange das Objekt aktiv ist
        {
            // Warte eine zufällige Zeitspanne basierend auf aktuellen Schwierigkeitsparametern
            float waitTime = Random.Range(currentMinSpawnDelay, currentMaxSpawnDelay);
            // Debug.Log($"Nächster Spawn in {waitTime:F2} Sekunden.");
            yield return new WaitForSeconds(waitTime);

            AttemptSpawn();
        }
    }

    void AttemptSpawn()
    {
        float randomX;
        int attempts = 0;

        // Versuche, eine X-Position zu finden, die nicht zu nah an der letzten ist
        do
        {
            randomX = Random.Range(minX, maxX);
            attempts++;
            // Wenn wir die maximale Anzahl von Versuchen erreicht haben oder es der allererste Spawn ist,
            // brechen wir die Schleife ab und nehmen den aktuellen randomX.
            if (attempts >= maxSpawnXAttempts || lastSpawnX == float.MinValue)
            {
                // if (attempts >= maxSpawnXAttempts && lastSpawnX != float.MinValue)
                // {
                //     Debug.LogWarning($"Max X-Attempts erreicht ({maxSpawnXAttempts}). Nehme X={randomX:F2} trotz möglicher Nähe zu letztem X={lastSpawnX:F2}.");
                // }
                break;
            }
        } while (Mathf.Abs(randomX - lastSpawnX) < minXDifference);

        lastSpawnX = randomX; // Aktualisiere für den nächsten Spawn

        Vector2 spawnPos = new Vector2(randomX, spawnY);

        // Entscheiden, ob Müll oder Asteroid gespawnt wird
        if (PlayerMovement.currentDifficultyLevel >= 2 && Random.value < currentAsteroidChance && asteroidPrefab != null)
        {
            SpawnItem(asteroidPrefab, spawnPos, "Asteroid", Color.gray);
        }
        else if (trashPrefab != null)
        {
            SpawnTrash(spawnPos);
        }
    }

    void SpawnTrash(Vector2 spawnPos)
    {
        int randomTrashType = Random.Range(0, 3); // 0: Yellow, 1: Blue, 2: Green
        // Debug.Log($"SpawnTrash - randomTrashType generiert: {randomTrashType} für Position X={spawnPos.x:F2}");

        switch (randomTrashType)
        {
            case 0:
                SpawnItem(trashPrefab, spawnPos, "Yellow", Color.yellow);
                break;
            case 1:
                SpawnItem(trashPrefab, spawnPos, "Blue", Color.blue);
                break;
            case 2:
                SpawnItem(trashPrefab, spawnPos, "Green", Color.green);
                break;
        }
    }

    void SpawnItem(GameObject prefabToSpawn, Vector2 position, string tag, Color color)
    {
        GameObject spawnedItem = Instantiate(prefabToSpawn, position, Quaternion.identity);
        spawnedItem.tag = tag;
        // Debug.Log($"GESPAWNT: Name={spawnedItem.name}, Tag={spawnedItem.tag}, Farbe ZUGEWIESEN={color}, Position X={position.x:F2}");

        SpriteRenderer sr = spawnedItem.GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = color;
        }

        TrashMovement tm = spawnedItem.GetComponent<TrashMovement>();
        if (tm != null)
        {
            tm.fallSpeed = currentFallSpeed;
        }
        else
        {
            Debug.LogWarning($"GameObject {prefabToSpawn.name} hat kein TrashMovement Skript!");
        }
    }
}