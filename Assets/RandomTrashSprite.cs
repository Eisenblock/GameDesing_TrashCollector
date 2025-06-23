using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class RandomSpriteSetter : MonoBehaviour
{
    [Header("Sprite-Liste")]
    public Sprite[] sprites;

    private SpriteRenderer sr;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();

        if (sprites == null || sprites.Length == 0)
        {
            Debug.LogWarning($"[{name}] Kein Sprite im Array! Kann nichts zuweisen.");
            return;
        }

        int randomIndex = Random.Range(0, sprites.Length);
        sr.sprite = sprites[randomIndex];

        Debug.Log($"[{name}] Zufälliges Sprite gesetzt: {sr.sprite.name}");
    }
}

