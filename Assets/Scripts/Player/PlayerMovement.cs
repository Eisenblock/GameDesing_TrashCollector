using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private float moveInput;
    private int stance_pos = 0; 

    [Header("Energy Settings")]
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyDrainPerSecond = 1f; // Wie viel Energie pro Sekunde verloren geht
    public float energyGainOnCorrect = 20f;
    public float energyLossOnIncorrect = 30f;
    public float energyLossOnAsteroid = 40f;

    [Header("Scoring & Difficulty")]
    private float score = 0;
    public static int currentDifficultyLevel = 0; // Static, damit SpawnObejct darauf zugreifen kann
    public int[] scoreThresholdsForDifficulty = { 500, 1500, 3000 }; // Score-Punkte für Level 1, 2, 3

    [Header("UI")]
    public TMP_Text EnergyValueText; 
    public TMP_Text ScoreValueText;  
    public TMP_Text DifficultyLevelText; // Neu für Anzeige des Levels

    [Header("Visuals & Cooldown")]
    public float stanceChangeCooldown = 0.5f;
    private float stanceChangeTimer = 0f;
    private bool isChangingStance = false;
    private bool gotHit = false;
    private Color originalColor; 

    private SpawnObejct spawner;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        currentEnergy = maxEnergy;
        stanceChangeTimer = 0f;
        UpdatePlayerColor();

        spawner = FindObjectOfType<SpawnObejct>();
        if (spawner == null)
        {
            Debug.LogError("SpawnObejct nicht in der Szene gefunden!");
        }
        UpdateDifficultyUI();
    }

    void Update()
    {
        // Bewegung
        moveInput = Input.GetAxisRaw("Horizontal");

        // Haltung wechseln
        if (Input.GetKeyDown(KeyCode.E) && !isChangingStance && !gotHit)
        {
            stance_pos = (stance_pos + 1) % 3;
            isChangingStance = true;
            stanceChangeTimer = stanceChangeCooldown;
            // Debug.Log("Stance changed to: " + stance_pos);
        }

        // Cooldown und Animation für Haltungswechsel
        if (isChangingStance)
        {
            stanceChangeTimer -= Time.deltaTime;
            transform.Rotate(0, 0, (360f / stanceChangeCooldown) * Time.deltaTime); // Dreht sich einmal komplett während Cooldown

            if (stanceChangeTimer <= 0)
            {
                stanceChangeTimer = 0f;
                isChangingStance = false;
                transform.rotation = Quaternion.identity; // Rotation zurücksetzen
                UpdatePlayerColor();
            }
        }

        // Energieverlust über Zeit
        if (currentEnergy > 0)
        {
            currentEnergy -= energyDrainPerSecond * Time.deltaTime;
            currentEnergy = Mathf.Max(0, currentEnergy); // Sicherstellen, dass Energie nicht negativ wird
        }

        UpdateUI();

        // Schwierigkeitslevel prüfen und anpassen
        CheckDifficultyUpdate();

        // Game Over Bedingung
        if (currentEnergy <= 0)
        {
            Debug.Log("Game Over! Score: " + score);
            // Reset static variable for next game
            currentDifficultyLevel = 0;
            SceneManager.LoadScene("MainMenu"); // Oder eine Game Over Szene
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);
    }

    void UpdatePlayerColor()
    {
        if (gotHit || isChangingStance) return; // Nicht Farbe ändern, wenn getroffen oder beim Wechseln

        switch (stance_pos)
        {
            case 0: // Blue
                GetComponent<SpriteRenderer>().color = Color.blue;
                originalColor = Color.blue;
                break;
            case 1: // Green
                GetComponent<SpriteRenderer>().color = Color.green;
                originalColor = Color.green;
                break;
            case 2: // Yellow
                GetComponent<SpriteRenderer>().color = Color.yellow;
                originalColor = Color.yellow;
                break;
        }
    }

    void UpdateUI()
    {
        if (EnergyValueText != null)
        {
            EnergyValueText.text = "Energy: " + Mathf.CeilToInt(currentEnergy).ToString();
        }
        if (ScoreValueText != null)
        {
            ScoreValueText.text = "Score: " + Mathf.FloorToInt(score).ToString();
        }
    }

    void UpdateDifficultyUI()
    {
        if (DifficultyLevelText != null)
        {
            DifficultyLevelText.text = "Difficulty: " + currentDifficultyLevel;
        }
    }


    void CheckDifficultyUpdate()
    {
        int newDifficultyLevel = 0;
        for (int i = 0; i < scoreThresholdsForDifficulty.Length; i++)
        {
            if (score >= scoreThresholdsForDifficulty[i])
            {
                newDifficultyLevel = i + 1;
            }
            else
            {
                break; // Da Schwellen sortiert sind
            }
        }

        if (newDifficultyLevel > currentDifficultyLevel)
        {
            currentDifficultyLevel = newDifficultyLevel;
            Debug.Log("Difficulty increased to level: " + currentDifficultyLevel);
            if (spawner != null)
            {
                spawner.UpdateDifficultyParameters();
            }
            UpdateDifficultyUI();
        }
    }

    public void DeductEnergyForMissedItem(float amountToDeduct)
    {
        if (currentEnergy > 0) // Nur abziehen, wenn noch Energie da ist
        {
            currentEnergy -= amountToDeduct;
            currentEnergy = Mathf.Max(0, currentEnergy); // Sicherstellen, dass Energie nicht negativ wird
            Debug.Log($"Verpasstes Item! Energie um {amountToDeduct} reduziert. Aktuelle Energie: {currentEnergy}");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (gotHit || isChangingStance) return; // Keine Kollisionsverarbeitung während Hit-Feedback oder Stance-Wechsel

        string collidedTag = collision.gameObject.tag;
        bool correctHit = false;

        if (collidedTag == "Asteroid")
        {
            currentEnergy -= energyLossOnAsteroid;
            Debug.Log("Hit by Asteroid! Energy lost: " + energyLossOnAsteroid);
        }
        else
        {
            switch (stance_pos)
            {
                case 0: // Blue stance
                    if (collidedTag == "Blue") correctHit = true;
                    break;
                case 1: // Green stance
                    if (collidedTag == "Green") correctHit = true; 
                    break;
                case 2: // Yellow stance
                    if (collidedTag == "Yellow") correctHit = true;
                    break;
            }

            if (correctHit)
            {
                score += 100;
                currentEnergy += energyGainOnCorrect;
                currentEnergy = Mathf.Min(currentEnergy, maxEnergy); // Energie nicht über Max
                Debug.Log("Correct Trash! Score: +100, Energy: +" + energyGainOnCorrect);
            }
            else // Falscher Müll oder nicht definierter Tag
            {
                currentEnergy -= energyLossOnIncorrect;
                Debug.Log("Incorrect Trash or unknown! Energy lost: " + energyLossOnIncorrect);
                // Visuelles Feedback für falschen Treffer
                GetComponent<SpriteRenderer>().color = Color.red;
                gotHit = true;
                Invoke(nameof(ResetColorAndHitState), 0.2f);
            }
        }

        Destroy(collision.gameObject); // Müll/Asteroid immer zerstören
    }

    void ResetColorAndHitState()
    {
        gotHit = false;
        if (!isChangingStance) // Nur Farbe zurücksetzen, wenn nicht gerade die Haltung gewechselt wird
        {
            UpdatePlayerColor();
        }
    }
}