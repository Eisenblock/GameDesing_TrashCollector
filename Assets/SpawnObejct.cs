using UnityEngine;

public class SpawnObejct : MonoBehaviour
{
    public GameObject trashPrefab;          // Prefab, das gespawnt wird
    public float spawnInterval = 2f;        // Alle wie viele Sekunden?
    public float spawnY = 10f;               // Fester Y-Wert
    public float minX = -8f, maxX = 8f;     // Bereich für X-Wert
    private float timer = 0;
    private float time_Checkpoints = 20;
    public float gravityScale = 0.1f;
    private int count_Dif = 0;
    private bool start_Asteroid = false;
    private float lastSpawnX = float.MinValue;
    private float lastRandSpawn = -1;
    private bool gotAngle = false;
    private float timeSpawn = 0;
    public GameObject backgroundPrefab;
    float nextBackgroundTime = 15f;

    void Start()
    {
        Debug.Log("Start() wurde ausgeführt in: " + gameObject.name);
        count_Dif = 0;
        gravityScale = 0.1f;
        time_Checkpoints = 15;
        start_Asteroid = false;
        gotAngle = false;
        Debug.Log("ResetLvL() ausgeführt: " +
     "count_Dif = " + count_Dif + ", " +
     "gravityScale = " + gravityScale + ", " +
     "time_Checkpoints = " + time_Checkpoints + ", " +
     "start_Asteroid = " + start_Asteroid + ", " +
     "gotAngle = " + gotAngle);
        //InvokeRepeating(nameof(SpawnTrash), 0f, spawnInterval);
    }

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer > time_Checkpoints)
        {
            gravityScale = gravityScale + 0.04f;
            time_Checkpoints = time_Checkpoints + 15;
            count_Dif += 1;
            if (count_Dif > 1)
            {
                start_Asteroid = true;
            }

            /*if (count_Dif > 2)
            {
                gotAngle = true;
            }*/
        }

        timeSpawn += Time.deltaTime;
        if (timeSpawn > spawnInterval) 
        {
            SpawnTrash();
            timeSpawn = 0;
        }

        if (timer >= nextBackgroundTime)
        {
            UpdateBackGround();
            nextBackgroundTime += 15f; // nächsten Zeitpunkt festlegen
        }

    }

    public void ResetLvL()
    {
        count_Dif = 0;
        gravityScale = 0.1f;
        time_Checkpoints = 15;
        start_Asteroid = false;
        gotAngle = false;
        Debug.Log("ResetLvL() ausgeführt: " +
            "count_Dif = " + count_Dif + ", " +
            "gravityScale = " + gravityScale + ", " +
            "time_Checkpoints = " + time_Checkpoints + ", " +
            "start_Asteroid = " + start_Asteroid + ", " +
            "gotAngle = " + gotAngle);
    }

    void SpawnTrash()
    {
        float randomX;
        int maxAttempts = 30;
        int attempts = 0;

        // Generiere gültige X-Position mit Mindestabstand 10 und Maximalabstand 60
        do
        {
            randomX = Random.Range(minX, maxX);
            attempts++;
            
        } while ((Mathf.Abs(randomX - lastSpawnX) < 5f || Mathf.Abs(randomX - lastSpawnX) > 40f) && attempts < maxAttempts);

        // Speichere neue Position
        lastSpawnX = randomX;
        float randomForTrash;
        if (lastRandSpawn == 3)
        {
            randomForTrash = Random.Range(0, 3);
            lastRandSpawn = -1;
        }
        else
        {
            randomForTrash = start_Asteroid ? Random.Range(0, 4) : Random.Range(0, 3);
        }

        Vector2 spawnPos = new Vector2(randomX, spawnY);

        GameObject spawnedTrash = Instantiate(trashPrefab, spawnPos, Quaternion.identity);
        Rigidbody2D rb = spawnedTrash.GetComponent<Rigidbody2D>();
        if (rb != null) rb.gravityScale = gravityScale;

        SpriteRenderer sr = spawnedTrash.GetComponent<SpriteRenderer>();
        if (gotAngle)
        {
            float angle = Random.Range(-20f, 20f);
            float speed = 1f;
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.down;
            rb.linearVelocity = dir * speed;
            rb.angularVelocity = Random.Range(-20f, 20f);
        }

        switch ((int)randomForTrash)
        {
            case 0:
                spawnedTrash.tag = "Yellow";
                sr.color = Color.yellow;
                break;
            case 1:
                spawnedTrash.tag = "Blue";
                sr.color = Color.blue;
                break;
            case 2:
                spawnedTrash.tag = "Grün";
                sr.color = Color.red;
                break;
            case 3:
                spawnedTrash.tag = "asteroid";
                sr.color = Color.gray;
                spawnedTrash.transform.localScale = new Vector3(8f, 8f, 1f);
                lastRandSpawn = 3;
                break;
        }
    }

    public void UpdateBackGround()
    {
        backgroundPrefab = Resources.Load<GameObject>("Prefabs/Background");
        Vector3 spawnPosition = new Vector3(0f, -200f, 0f);
        Instantiate(backgroundPrefab, spawnPosition, Quaternion.identity);
    }
}
