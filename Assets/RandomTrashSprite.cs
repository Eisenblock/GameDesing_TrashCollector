using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteSetter : MonoBehaviour
{
    [Header("Sprite-Arrays")]
    public Sprite[] sprites1;
    public Sprite[] sprites2;
    public Sprite[] sprites3;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        // Alle Arrays in ein 2D-Array packen
        Sprite[][] allSpriteArrays = new Sprite[][] { sprites1, sprites2, sprites3 };

        // Zuf�lliges Array ausw�hlen
        int randomArrayIndex = Random.Range(0, allSpriteArrays.Length);
        Sprite[] selectedArray = allSpriteArrays[randomArrayIndex];

        if (selectedArray == null || selectedArray.Length == 0)
        {
            Debug.LogWarning($"[{name}] Das gew�hlte Sprite-Array ist leer oder null.");
            return;
        }

        // Tag je nach ausgew�hltem Array setzen
        switch (randomArrayIndex)
        {
            case 0:
                gameObject.tag = "Gr�n";
                break;
            case 1:
                gameObject.tag = "Yellow";
                break;
            case 2:
                gameObject.tag = "Blue";
                break;
        }

        // Zuf�lliges Sprite aus dem gew�hlten Array ausw�hlen
        int randomSpriteIndex = Random.Range(0, selectedArray.Length);
        sr.sprite = selectedArray[randomSpriteIndex];

        Debug.Log($"[{name}] Sprite aus Array {randomArrayIndex + 1} gesetzt: {sr.sprite.name} (Tag: {gameObject.tag})");
    }
}
